using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GME1011_A2_Part2_ChloeF;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _font;

    private Ped _ped;
    private Texture2D _pedTexture;

    private List<Vehicle> _vehicles;
    private Texture2D _carTexture;
    private Texture2D _truckTexture;
    private Random _rng;

    private Carrot _carrot;
    private Texture2D _carrotTexture;

    private bool _isFrozen = false;
    private float _freezeTimer = 0f;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = 800;
        _graphics.PreferredBackBufferHeight = 600;
        _graphics.ApplyChanges();

        _rng = new Random();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _font = Content.Load<SpriteFont>("GameFont");

        _pedTexture = Content.Load<Texture2D>("playerPed");
        _carTexture = Content.Load<Texture2D>("carImg");
        _truckTexture = Content.Load<Texture2D>("truckImg");
        _carrotTexture = Content.Load<Texture2D>("carrotImg");

        _carrot = new Carrot(_carrotTexture, new Vector2(_rng.Next(100, 700), 50));

        _vehicles = new List<Vehicle>();

        float[] laneY = { 150, 230, 390, 470 };

        foreach (float y in laneY)
        {
            int dir = (y < 300) ? -1 : 1;

            float laneSpeed;
            if (y == 230 || y == 390)
            {
                laneSpeed = 5.5f;
            }
            else
            {
                laneSpeed = 2.5f;
            }

            int numVehicles = _rng.Next(3, 5);
            float nextX = _rng.Next(0, 200);

            for (int i = 0; i < numVehicles; i++)
            {
                Color color = new Color(_rng.Next(256), _rng.Next(256), _rng.Next(256), 255);

                if (y == 230 || y == 390)
                {
                    _vehicles.Add(new Car(_carTexture, new Vector2(nextX, y), laneSpeed, dir, color));
                }
                else
                {
                    _vehicles.Add(new Truck(_truckTexture, new Vector2(nextX, y), laneSpeed, dir, color));
                }

                nextX += 300f;
            }
        }

        Color pedColor = new Color(_rng.Next(256), _rng.Next(256), _rng.Next(256), 255);
        _ped = new Ped(pedColor, 4.5f, 1.0f, false, _pedTexture);
    }

    protected override void Update(GameTime gameTime)
    {
        if (_isFrozen)
        {
            _freezeTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_freezeTimer >= 0.5f)
            {
                _isFrozen = false;
                _freezeTimer = 0f;
                RestartGame();
            }
            return;
        }

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _ped.Update();

        foreach (Vehicle v in _vehicles)
        {
            v.Update();

            if (v.GetCollisionRect().Intersects(_ped.GetCollisionRect()))
            {
                _isFrozen = true;
                _freezeTimer = 0f;
                return;
            }
        }

        if (_ped.GetCollisionRect().Intersects(_carrot.GetCollisionRect()))
        {
            if (_carrot.IsTop())
            {
                _carrot.SetPosition(new Vector2(_rng.Next(100, 700), 550));
                _carrot.SetIsTop(false);
            }
            else
            {
                _carrot.SetPosition(new Vector2(_rng.Next(100, 700), 50));
                _carrot.SetIsTop(true);
            }
        }

        foreach (float y in new float[] { 150, 230, 390, 470 })
        {
            List<Vehicle> laneVehicles = new List<Vehicle>();

            foreach (Vehicle v in _vehicles)
            {
                if (v.GetPosition().Y == y)
                    laneVehicles.Add(v);
            }

            laneVehicles.Sort((a, b) =>
            {
                if (a.GetDirection() == 1)
                    return a.GetPosition().X.CompareTo(b.GetPosition().X);
                else
                    return b.GetPosition().X.CompareTo(a.GetPosition().X);
            });

            for (int i = 1; i < laneVehicles.Count; i++)
            {
                Vehicle front = laneVehicles[i - 1];
                Vehicle back = laneVehicles[i];

                float gap = Math.Abs(back.GetPosition().X - front.GetPosition().X);

                if (gap < 160f)
                {
                    Vector2 pos = back.GetPosition();
                    if (back.GetDirection() == 1)
                        pos.X = front.GetPosition().X - 160f;
                    else
                        pos.X = front.GetPosition().X + 160f;

                    back.SetPosition(pos);
                }
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.LightSlateGray);

        _spriteBatch.Begin();

        _ped.Draw(_spriteBatch);

        foreach (Vehicle v in _vehicles)
        {
            v.Draw(_spriteBatch);
        }

        _carrot.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void RestartGame()
    {
        LoadContent();
    }
}
