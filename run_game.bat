dotnet build

halite.exe --replay-directory replays/ -vvv --width 32 --height 32 "dotnet %cd%\bin\Debug\netcoreapp2.1\HaliteGameBot.dll" "dotnet %cd%\refBot\HaliteGameBot.dll"
