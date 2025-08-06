using Microsoft.Extensions.Logging;

namespace Mayonyies.Application.Messaging.Behaviors;

internal static class LoggingDecorator
{
    internal sealed class CommandHandler<TCommand>(
        ICommandHandler<TCommand> innerHandler,
        ILogger<CommandHandler<TCommand>> logger
    ) : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var requestName = typeof(TCommand).Name;
            
            logger.LogInformation("Handling command: {RequestName}", requestName);
            
            var result = await innerHandler.Handle(command, cancellationToken);
            
            if(result.IsSuccess)
                logger.LogInformation("Completed command: {RequestName}", requestName);
            else
                logger.LogError("Completed command {RequestName} with error {Error}", requestName, result.Error);

            return result;
        }
    }
    
    internal sealed class CommandHandler<TCommand, TResponse>(
        ICommandHandler<TCommand, TResponse> innerHandler,
        ILogger<CommandHandler<TCommand, TResponse>> logger
    ) : ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        public async Task<Result<TResponse>> Handle(TCommand query, CancellationToken cancellationToken)
        {
            var requestName = typeof(TCommand).Name;
            
            logger.LogInformation("Handling command: {RequestName}", requestName);
            
            var result = await innerHandler.Handle(query, cancellationToken);
            
            if(result.IsSuccess)
                logger.LogInformation("Completed command: {RequestName}", requestName);
            else
                logger.LogError("Completed command {RequestName} with error {Error}", requestName, result.Error);

            return result;
        }
    }
    
    internal sealed class QueryHandler<TQuery, TResponse>(
        IQueryHandler<TQuery, TResponse> innerHandler,
        ILogger<QueryHandler<TQuery, TResponse>> logger
        ) : IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken)
        {
            var requestName = typeof(TQuery).Name;
            
            logger.LogInformation("Handling query: {RequestName}", requestName);
            
            var result = await innerHandler.Handle(query, cancellationToken);
            
            if(result.IsSuccess)
                logger.LogInformation("Completed request: {RequestName}", requestName);
            else
                logger.LogError("Completed request {RequestName} with error {Error}", requestName, result.Error);

            return result;
        }
    }
}