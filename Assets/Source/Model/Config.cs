namespace FireBalls3D.Model
{
    public class Config
    {
        public static int TankHealth => 10;
        public static float GunReload => 0.2f;
        public static int Damage => 1;
        public static float RecoilDistance => 0.2f;
        public static float RecoilDuration => GunReload * 0.9f;
        public static float ShakeDuration => 0.1f;
        public static float ShakeDistance => 0.1f;
        public static int MissileLifeTimeInSeconds => 1;
        public static int MissileFlySpeed = 45;
        public static float MinObstacleDegreePerUnit = 50f;
        public static float MaxObstacleDegreePerUnit = 100f;
        public static float DistanceBetweenObstacles = 1.5f;
        public static float OffsetFromRotateCenter = 3f;
        public static int NumberObstacleLevels = 2;
        public static int NumberSegments = 5;
        public static int Reward = 3;
    }
}
