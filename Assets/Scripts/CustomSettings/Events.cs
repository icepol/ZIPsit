namespace pixelook
{
    public static class Events
    {
        public const string SKIN_LEFT_BUTTON_CLICK = "SkinLeftButtonClick";
        public const string SKIN_RIGHT_BUTTON_CLICK = "SkinRightButtonClick";

        // level progression events
        public const string GAME_STARTED = "GameStarted";
        public const string LEVEL_CHANGED = "LevelChanged";
        public const string SCORE_CHANGED = "ScoreChanged";
        public const string GAME_FINISHED = "GameFinished";

        public const string ZIPS_ORIENTATION_SELECTED = "ZipsOrientationSelected";
        public const string ZIPS_MOVE_STARTED = "ZipsMoveStarted";
        public const string ZIPS_MOVE_FINISHED = "ZipsMoveFinished";
        public const string ZIPS_EXPLODED = "ZipsExploded";
        
        public const string PLAYER_DIED = "PlayerDied";
        public const string PLAYER_COLLIDED_OBSTACLE = "PlayerCollidedObstacle";
        public const string PLAYER_GATE_PASSED = "PlayerGatePassed";
        public const string PLAYER_GATE_DESTROYED = "PlayerGateDestroyed";
        public const string PLAYER_ROTATION_CHANGED = "PlayerRotationChange";
        public const string PLAYER_POSITION_CHANGED = "PlayerPositionChange";
        public const string PLAYER_SKIN_CHANGED = "SkinChanged";

        // camera events
        public const string CAMERA_SHAKE_BIG = "CameraShakeBig";
        public const string CAMERA_SHAKE_SMALL = "CameraShakeSmall";
        
        // settings events
        public const string MUSIC_SETTINGS_CHANGED = "MusicSettingsChanged";
        public const string PURCHASE_FINISHED = "PurchaseFinished";
        public const string PANEL_SHOW = "PanelVisible";
        public const string PANEL_HIDE = "PanelHide";
    }
}