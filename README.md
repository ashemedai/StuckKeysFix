# Stuck Keys Fix

## Setup

.NET SDK 4.6 or higher.

- Create a `libs` directory.
- Copy `Graveyard Keeper\BepInEx\core\BepInEx.dll` to `libs`.
- Copy from `Graveyard Keeper_Data\Managed` these DLLs to `libs`:
  - `UnityEngine.dll`
  - `UnityEngine.CoreModule.dll`
  - `UnityEngine.InputLegacyModule.dll`

## Compilation

Run `dotnet build -c Release` from the command line.

## Installation

Ensure that [Graveyard Keeper BepInEx 5 Pack](https://www.nexusmods.com/graveyardkeeper/mods/79)
is installed.

Copy the compiled DLL to `Graveyard Keeper\BepInEx\plugins\StuckKeysFix\`.
Or download it from [Nexus Mods](https://www.nexusmods.com/graveyardkeeper/mods/152).

The mod wil log to `Player.log` (found in
`%AppData%\..\LocalLow\Lazy Bear Games\Graveyard Keeper\`).
