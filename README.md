### Description:
This project is a simple clone of the original Pac-Man arcade game. While it aims to be similar to the original, it is not an exact copy.

The main goal of this project is to showcase my skills as a C# programmer and Unity developer in a small project where the code can be easily analyzed.

### Execution:
Unity Version: Unity 6.0.25f1

Open the "Game" or "Init" scene in the following folder:
-PacMan/Assets/Scenes/Game.unity
-PacMan/Assets/Scenes/Init.unity

### Required Dependencies:
- Zenject
- TextMesh Pro

### Controls:
Pac-Man can be controlled using the WASD keys or the arrow keys.

### Liks:
WebGL executable on Itch.io:

https://moonthsoft.itch.io/pacman

Repository on GitHub:

https://github.com/moonthsoft/PacMan

### Features:
- Character movement through the maze.	
- Ghost AI with different states and unique behaviors for each ghost.
- Various items such as dots and power-ups.
- Game logic, including player death, level completion, and increasing difficulty as levels progress.
- Scoring system.
- User interface displaying the current level, player lives, current score, and high score.
- Various animations for Pac-Man and the ghosts.
- Audio, with dynamic music and sound effects that change based on the game's state.

### Project Structure:
The project is located in the Assets folder, with most subfolders divided into two main sections: Core and PacMan.

Core contains generic elements that can be used in multiple games. PacMan contains elements specific to this project.

### The project's main folders are:
- __Animations:__ Contains animations and animator controllers for the game.
- __Art:__ Contains fonts, materials, shaders, and sprites. The custom shader ShaderColorTransparent is used to ignore background colors in sprites that lack transparency.
- __Audio:__ Contains the game's sound effects and music.
- __Editor:__ Special Unity folder used for editor-related classes and functions. In this project, it contains the custom class SerializableMatrixDrawer.
- __Plugins:__ Contains third-party plugins used in the project, such as Zenject and TextMesh Pro.
- __Prefabs:__ Includes game prefabs, such as managers, ghosts, items, and the graph system for pathfinding.
- __Resources:__ Special Unity folder where the ProjectContext prefab is stored to enable dependency injection for managers.
- __Scenes:__ Contains the various game scenes:
	- __Game:__ The main gameplay scene.
	- __Init:__ The first scene executed, which simply loads the Game scene.
	- __Loading:__ A temporary scene used when switching between scenes to avoid loading two at once.
	- __MainMenu:__ Currently unused, intended for the main menu (not yet implemented).
- __Score:__ Currently unused, intended for the post-game score display.
- __ScriptableObjects:__ Currently contains only the Configuration scriptable object, which is used to configure various game values such as Pac-Man and ghost speeds, and scoring.
- __Scripts:__ Contains all game scripts.
- __Settings:__ Default Unity folder for configuring URP (Unity's current rendering pipeline).

### Code Architecture:
The game architecture is based on managers, accessed through dependency injection using Zenject. The main managers are:
- __AudioManager:__ Handles sound effects and music playback.
- __DataManager:__ Stores persistent data, such as player scores.
- __InputManager:__ Handles player input.
- __LoadSceneManager:__ Manages scene transitions.
- __LevelManager:__ Manages game elements like Pac-Man, ghosts, and the graph, with game logic divided into subcomponents: GameLogic, Items, Music, Score y Timer.

### Artificial Intelligence:
The ghosts' AI is based on a finite state machine (FSM). Each ghost has multiple states and a unique personality, achieved through different behaviors in the Chase state.

Additionally, ghosts use a simple pathfinding system based on a graph, calculating the shortest path to a target node without turning 180°.
		
### Ghost states:
- __Eaten:__ Activated when the ghost is eaten by Pac-Man; the ghost returns to its respawn point.
- __Frightened:__ Triggered when the player collects a power-up. The ghost first turns 180° and then moves randomly at intersections.
- __Home:__ Initial state where the ghost waits for a specific duration before leaving the spawn area.
- __Scatter:__ The ghost moves to its designated corner and loops around. Ghosts alternate between Scatter and Chase at fixed intervals to control difficulty.
- __Chase:__ The main state where ghosts pursue Pac-Man, each using a different strategy:
  - __BlinkyChase:__ Blinky (red) directly follows Pac-Man’s last known position.
  - __PinkyChase:__ Pinky (pink) moves towards a position ahead of Pac-Man to cut him off.
  - __InkyChase:__ Inky (blue) moves toward a calculated position based on Blinky’s and Pac-Man’s positions.
  - __ClydeChase:__ Clyde (orange) behaves like Blinky but switches to Scatter mode if too close to Pac-Man.

### User Interface:
The UI follows a Model-View-Controller (MVC) architecture:
- __LevelManager (Controller):__ Handles game logic and updates UI elements.
- __DataManager (Model):__ Stores and manages player data, such as scores.
- __LevelUI (View):__ Displays the UI elements (e.g., score, lives).

### Game Scene Structure:
In the Game scene hierarchy, the following elements can be found:
- __Main Camera:__ A fixed camera with no additional logic.
- __Global Light 2D:__ Required by Unity’s URP rendering pipeline.
- __SceneContext:__ Handles dependency injection for managers using Zenject.
- __Background:__ The game's static background sprite.
- __BackgroundWithElements:__ A reference sprite showing dots and power-ups for placement. Disabled in the final game.
- __LevelManager:__ Manages various game logic components.
- __UI:__ The game's UI canvas, displaying lives, score, and level. Also handles in-game text like "Ready!" and score popups when eats a ghost, for that reason uses Screen Space - Camera mode.
- __Graph:__ The node-based movement system used by both Pac-Man and the ghosts. This ensures smooth movement and enables ghost pathfinding.
- __ElementsScene:__ Contains scene elements such as dots and power-ups.
- __Player:__ The Pac-Man character controlled by the player.
- __Ghosts:__ Contains all four ghosts.
- __EventSystem:__ Default Unity component for UI navigation (unused in this project).

### Credits:
This project was developed by Antonio García Tortosa.

Audio and sprites were sourced from the original Pac-Man arcade game.
