# tradmeup
A trading bot using alpaca markets api, written in csharp.

# Follow along
Streaming the progress on https://www.youtube.com/channel/UCJoqffHqDCoSp-hxGdp1K9A

# Configuration File
The ConfigurationManager retrieves a configuration file based on the build configuration and the base path
is set to the current runtime directory.  The configuration file will contain the following that can be modifed:
- live: bool: Whether to run in live or paper mode
- liveId: string: Your alpaca live id
- liveKey: string: Your alpaca live key
- paperId: string: Your alpaca paper id
- paperKey: string: Your alpaca paper key
- strategies: string array: The strategy names that are to be loaded at runtime
- extendedTrading: bool: Whether extended trading hours is enabled

There is a sample appsettings.json copied as content into the build
directory.  You can copy and paste that file, rename with one of the following below, and fill in your details.
- Debug configuration uses dev.appsettings.json
- Release configuration uses prod.appsettings.json