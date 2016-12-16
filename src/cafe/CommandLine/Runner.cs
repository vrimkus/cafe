﻿using Microsoft.Extensions.Logging;

namespace cafe.CommandLine
{
    public class Runner
    {
        private static ILogger Logger { get; } =
          ApplicationLogging.CreateLogger<Runner>();

        private readonly Option[] _options;

        public Runner(params Option[] options)
        {
            _options = options;
        }

        public void Run(params string[] args)
        {
            var matchingOption = MatchingOptionFor(args);

            if (matchingOption != null)
            {
                Logger.LogInformation($"Option {matchingOption} matches the arguments supplied, so running");
                matchingOption.Run(args);
                Logger.LogInformation($"Finished running option {matchingOption}");
            }
            else
            {
                Logger.LogError("Since there are no matching options, aborting");
            }
        }

        private Option MatchingOptionFor(string[] args)
        {
            foreach (var option in _options)
            {
                if (option.IsSatisfiedBy(args))
                {
                    Logger.LogDebug($"Option {option} matches arguments supplied");
                    return option;
                }
                else
                {
                    Logger.LogInformation($"Option {option} does not satisfy the arguments supplied");
                }
            }
            return null;
        }
    }
}