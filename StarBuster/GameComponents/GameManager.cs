using StarBuster.Objects2D;
using StarBuster.Types;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace StarBuster.GameComponents
{
    public class GameManager
    {
        public int Width;           // Game screen width
        public int Height;          // Game screen height
        public HashSet<Keys> KeySet;  // Set of pressed keys
        public int FrameIndex;
        public GameState State;
        public int Difficulty;     // Game difficulty (1: Easy, 2: Medium, 3: Hard)

        private List<Object2D> _objects;    // List of active game objects
        private List<Object2D> _toAdd;      // Objects to add in the next frame
        private List<Object2D> _toRemove;   // Objects to remove in the next frame
        private CollisionDetector _detector; // Collision detection object
        private CollisionSolver _solver;     // Collision resolution object

        private static readonly GameManager _instance = new GameManager();
        public static GameManager Instance => _instance;

        public int ObjectCount => _objects.Count;

        private GameManager()
        {
            _objects = new List<Object2D>
            {
                new StarField(200, 1200, 800),
                new Hero(100, 100),
            };

            _toAdd = new List<Object2D>();
            _toRemove = new List<Object2D>();

            KeySet = new HashSet<Keys>();

            _detector = new CollisionDetector(_objects);
            _solver = new CollisionSolver();
            FrameIndex = 0;
            State = GameState.TitleScreen;
            Difficulty = 2; // Default difficulty: Medium
        }

        public void AddObject2D(Object2D obj)
        {
            _toAdd.Add(obj);
        }

        public void Remove(Object2D obj)
        {
            _toRemove.Add(obj);
        }

        public void Render(Graphics g)
        {
            // Render StarField in all game states
            _objects[0].Render(g);

            switch (State)
            {
                case GameState.TitleScreen:
                    RenderTitleScreen(g);
                    break;
                case GameState.GamePlay:
                    RenderGamePlay(g);
                    break;
                case GameState.Options:
                    RenderOptions(g);
                    break;
                case GameState.GameOver:
                    RenderGameOver(g);
                    break;
                case GameState.About:
                    RenderAbout(g);
                    break;
            }
        }

        private void RenderGamePlay(Graphics g)
        {
            foreach (Object2D obj in _objects)
            {
                if (obj is not StarField)
                    obj.Render(g);
            }
        }

        private void RenderTitleScreen(Graphics g)
        {
            Font drawFont = new Font("Verdana", 100, FontStyle.Bold);
            SolidBrush drawBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
            g.DrawString("StarBuster v1.0", drawFont, drawBrush, 50, 50);

            if (FrameIndex % 40 < 20)
            {
                drawFont = new Font("Verdana", 40);
                drawBrush = new SolidBrush(Color.FromArgb(255, 255, 0)); // Yellow for button text
                g.DrawString("Play [SPACE]", drawFont, drawBrush, 300, 450);
                g.DrawString("Options [O]", drawFont, drawBrush, 300, 500);
                g.DrawString("About [A]", drawFont, drawBrush, 300, 550);
                g.DrawString("Exit [Q]", drawFont, drawBrush, 300, 600); // Exit game option
            }
        }

        private void RenderOptions(Graphics g)
        {
            Font drawFont = new Font("Verdana", 40, FontStyle.Bold);
            SolidBrush drawBrush = new SolidBrush(Color.FromArgb(255, 255, 0)); // Bright color
            g.DrawString("Game Options", drawFont, drawBrush, 200, 100);

            drawFont = new Font("Verdana", 20);
            g.DrawString($"Difficulty Level: {(Difficulty == 1 ? "Easy" : Difficulty == 2 ? "Medium" : "Hard")}", drawFont, drawBrush, 100, 200);
            g.DrawString("Return to Menu [ESC]", drawFont, drawBrush, 100, 250);
        }

        private void RenderGameOver(Graphics g)
        {
            Font drawFont = new Font("Verdana", 80, FontStyle.Bold);
            SolidBrush drawBrush = new SolidBrush(Color.FromArgb(255, 0, 0)); // Red for "Game Over"
            g.DrawString("GAME OVER", drawFont, drawBrush, 100, 200);

            drawFont = new Font("Verdana", 30);
            drawBrush = new SolidBrush(Color.FromArgb(255, 255, 255)); // White for instructions
            g.DrawString("Play Again [R]", drawFont, drawBrush, 100, 400);
            g.DrawString("Return to Menu [ESC]", drawFont, drawBrush, 100, 450);
        }

        private void RenderAbout(Graphics g)
        {
            Font drawFont = new Font("Verdana", 40, FontStyle.Bold);
            SolidBrush drawBrush = new SolidBrush(Color.FromArgb(255, 0, 0)); // Red for "About"
            g.DrawString("About StarBuster", drawFont, drawBrush, 200, 100);

            drawFont = new Font("Verdana", 20);
            g.DrawString("Version: 1.0", drawFont, drawBrush, 100, 200);
            g.DrawString("Author: Kacper Porębski", drawFont, drawBrush, 100, 250);
            g.DrawString("Return to Menu [ESC]", drawFont, drawBrush, 100, 300);
        }

        public void Update()
        {
            switch (State)
            {
                case GameState.TitleScreen:
                    _objects[0].Update();
                    if (KeySet.Contains(Keys.Space)) State = GameState.GamePlay;
                    if (KeySet.Contains(Keys.O)) State = GameState.Options;
                    if (KeySet.Contains(Keys.A)) State = GameState.About;
                    if (KeySet.Contains(Keys.Q)) Application.Exit(); // Exit game
                    break;

                case GameState.GamePlay:
                    UpdateGamePlay();
                    if (CheckGameOverCondition()) State = GameState.GameOver;
                    break;

                case GameState.Options:
                    UpdateOptions();
                    break;

                case GameState.About:
                    _objects[0].Update();
                    if (KeySet.Contains(Keys.Escape)) State = GameState.TitleScreen;
                    break;

                case GameState.GameOver:
                    _objects[0].Update();
                    if (KeySet.Contains(Keys.R))
                    {
                        RestartGame();
                        State = GameState.GamePlay;
                    }
                    if (KeySet.Contains(Keys.Escape)) State = GameState.TitleScreen;
                    break;
            }

            FrameIndex++;
        }

        private void UpdateOptions()
        {
            if (KeySet.Contains(Keys.D1)) Difficulty = (Difficulty % 3) + 1; // Cycle through difficulty levels
            if (KeySet.Contains(Keys.Escape)) State = GameState.TitleScreen;
        }

        private void UpdateGamePlay()
        {
            foreach (Object2D obj in _objects) obj.Update();

            var collisions = _detector.DetectCollisions();
            _solver.ResolveCollisions(collisions);

            _objects.AddRange(_toAdd);
            _toAdd.Clear();

            foreach (var obj in _objects)
            {
                if (obj.IsOutOfScreen(Width, Height) && obj is not Hero)
                {
                    _toRemove.Add(obj);
                }
            }

            _objects.RemoveAll(obj => _toRemove.Contains(obj));
            _toRemove.Clear();
            Object2DSpawner.Update(FrameIndex);
        }

        private bool CheckGameOverCondition()
        {
            var hero = _objects.Find(o => o is Hero) as Hero;
            return hero != null && hero._energy <= 0;
        }

        private void RestartGame()
        {
            _objects.Clear();
            _toAdd.Clear();
            _toRemove.Clear();

            _objects.Add(new StarField(200, 1200, 800));
            _objects.Add(new Hero(100, 100));
        }

        public void SetResolution(int aWidth, int aHeight)
        {
            Width = aWidth;
            Height = aHeight;
        }
    }
}
