# Space Survival

This is a simple 2D space shooter game built using Unity. The game features a player-controlled ship that can move and shoot, as well as enemy ships that move and fire back at the player. The game's scoring system is seeing how long the player can survive and records it in seconds.

## Getting Started

To get started with the game, simply clone the repository and open the project in Unity. The game can be built and run on any platform that Unity supports.

## Code Overview

The codebase is organized into several scripts that handle different aspects of the game. Here's a brief overview of each script:

- `PlayerShipController.cs`: Handles the movement, firing, and destruction of the player's ship. Includes scoring as well.
- `EnemyShip.cs`: Handles the movement, firing, and destruction of enemy ships.
- `EnemyWaveSpawner.cs`: Spawns waves of enemy ships.
- `WaveConfig.cs`: Defines the properties of each wave of enemy ships.
- `BulletController.cs`: Handles collisions between bullets and other game objects.
- `EnemyBulletController.cs`: Handles collisions between enemy bullets and other game objects.
- `ScrollBackground.cs`: Scrolls a background image infinitely.
- `TutorialText.cs`: Handles the movement and destruction of tutorial text.
- `TitleScreenController.cs`: Handles the "Start" button click event and loads the main game scene.

## Contributing

Contributions to the game are welcome! If you'd like to contribute, simply fork the repository and submit a pull request with your changes.

## License

This game is licensed under the MIT License. See the `LICENSE` file for more information.
