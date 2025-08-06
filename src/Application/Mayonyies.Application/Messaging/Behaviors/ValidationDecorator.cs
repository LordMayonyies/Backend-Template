using FluentValidation;
using FluentValidation.Results;
using Mayonyies.Core;

namespace Mayonyies.Application.Messaging.Behaviors;

internal static class ValidationDecorator
{
    internal sealed class CommandHandler<TCommand>(
        ICommandHandler<TCommand> innerHandler,
        IEnumerable<IValidator<TCommand>> validators
    ) : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var validationFailures = await ValidateAsync(command, validators);
            
            if(validationFailures.Length == 0)
                return await innerHandler.Handle(command, cancellationToken);
            
            return Result.Failure(CreateValidationError(validationFailures));
        }
    }
    
    internal sealed class CommandHandler<TCommand, TResponse>(
        ICommandHandler<TCommand, TResponse> innerHandler,
        IEnumerable<IValidator<TCommand>> validators
    ) : ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var validationFailures = await ValidateAsync(command, validators);
            
            if(validationFailures.Length == 0)
                return await innerHandler.Handle(command, cancellationToken);
            
            return CreateValidationError(validationFailures);
        }
    }
    
    private static async Task<ValidationFailure[]> ValidateAsync<TCommand>(TCommand command, IEnumerable<IValidator<TCommand>> validators)
    {
        if (!validators.Any())
            return [];
            
        var context = new ValidationContext<TCommand>(command);
            
        var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context)));
            
        return validationResults
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .ToArray();
    }
    
    private static Error CreateValidationError(ValidationFailure[] validationFailures) =>
        Errors.Validation.Create(validationFailures.Select(f => new Error(f.ErrorCode, f.ErrorMessage)).ToArray());
}