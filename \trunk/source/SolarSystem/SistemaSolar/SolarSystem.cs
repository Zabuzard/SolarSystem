/*
 * Licensed under 'The Code Project Open License (CPOL)' (http://www.codeproject.com/info/cpol10.aspx)
 * This code is original from Vasily Tserekh and was modified by Daniel Tischner.
 * Modifications include:
 * - heavily more security
 * - common standard naming
 * - possibility to add new custom planets
 * - more content and several bug fixes
*/

using System;
using System.Collections.Generic;

namespace SolarSystem
{
    enum Planets
    { Mercury, Venus, Earth, Mars, Jupiter, Saturn,
        Neptune, Uranus, Pluto, Custom }

    internal struct Position
    {
        internal float mX;
        internal float mY;
        internal float mZ;

        internal Position(int x, int y, int z)
        {
            mX = x;
            mY = y;
            mZ = z;
        }
    }

    internal sealed class SolarSystem
    {
        readonly Star mStar = new Star();
        readonly Sun mSun = new Sun();
        readonly List<Planet> mPlanets = new List<Planet>();

        public void CreateScene()
        {
            mPlanets.Add(new Planet(0.5f, Planets.Mercury, new Position(10, 0, 0), "mercury.jpg"));
            mPlanets.Add(new Planet(0.7f, Planets.Venus, new Position(18, 0, 0), "venus.jpg"));
            mPlanets.Add(new Planet(1, Planets.Earth, new Position(32, 0, 0), "earth.jpg"));
            mPlanets.Add(new Planet(1, Planets.Mars, new Position(36, 0, 0), "mars.jpg"));
            mPlanets.Add(new Planet(1.5f, Planets.Jupiter, new Position(44, 0, 0), "jupiter.jpg"));
            mPlanets.Add(new Planet(1.2f, Planets.Saturn, new Position(56, 0, 0), "saturn.jpg"));
            mPlanets.Add(new Planet(1.2f, Planets.Uranus, new Position(64, 0, 0), "uranus.jpg"));
            mPlanets.Add(new Planet(1.2f, Planets.Neptune, new Position(67, 0, 0), "neptune.jpg"));
            mPlanets.Add(new Planet(1.2f, Planets.Pluto, new Position(89, 0, 0), "pluto.jpg"));

            mStar.CreateStars(500);
            mSun.Create();
            foreach (var item in mPlanets)
            {
                item.Create();
            }
        }

        public void DrawScene(bool stopAnim)
        {
            mStar.Draw();
            mSun.Paint(stopAnim);

            foreach (var item in mPlanets)
            {
                item.Paint(stopAnim);
            }
        }

        public void AddPlanet(float radius, int distance, String texture)
        {
            var planet = new Planet(radius, Planets.Custom,
                new Position(distance, 0, 0), texture);
            mPlanets.Add(planet);
            planet.Create();
        }
    }
}
