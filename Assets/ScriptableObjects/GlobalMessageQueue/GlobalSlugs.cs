using UnityEditor;

public static class GlobalSlugs
{
    public static readonly string PRE_BATTLE_STARTED = nameof(PRE_BATTLE_STARTED);
    public static readonly string BATTLE_STARTED = nameof(BATTLE_STARTED);
    public static readonly string BATTLE_ENDED = nameof(BATTLE_ENDED);

    public static readonly string PLAYER_ADDED_TO_BATTLE = nameof(PLAYER_ADDED_TO_BATTLE);
    public static readonly string PLAYER_REMOVED_FROM_BATTLE = nameof(PLAYER_REMOVED_FROM_BATTLE);
    public static readonly string PLAYER_STATE_CHANGED = nameof(PLAYER_STATE_CHANGED);
    public static readonly string PLAYER_HEALTH_CHANGED = nameof(PLAYER_HEALTH_CHANGED);
    public static readonly string PLAYER_HIT = nameof(PLAYER_HIT);
    public static readonly string PLAYER_DIED = nameof(PLAYER_DIED);
    public static readonly string PLAYER_ATTACKED = nameof(PLAYER_ATTACKED);
    public static readonly string PLAYER_DAMAGED = nameof(PLAYER_DAMAGED);
    public static readonly string PLAYER_POSITION_CHANGED = nameof(PLAYER_POSITION_CHANGED);
    public static readonly string PLAYER_ROTATION_CHANGED = nameof(PLAYER_ROTATION_CHANGED);
    public static readonly string PLAYER_ACQUIRED_TARGET = nameof(PLAYER_ACQUIRED_TARGET);
    public static readonly string PLAYER_TARGETED = nameof(PLAYER_TARGETED);
    public static readonly string PLAYER_WON = nameof(PLAYER_WON);

    public static readonly string PRE_BATTLE_ENDED = nameof(PRE_BATTLE_ENDED);
    public static readonly string BATTLE_ENDED_IN_DRAW = nameof(BATTLE_ENDED_IN_DRAW);

    public static readonly string WEAPON_ADDED_TO_BATTLE = nameof(WEAPON_ADDED_TO_BATTLE);
    public static readonly string WEAPON_REMOVED_FROM_BATTLE = nameof(WEAPON_REMOVED_FROM_BATTLE);
    public static readonly string WEAPON_STATE_CHANGED = nameof(WEAPON_STATE_CHANGED);
    public static readonly string WEAPON_FIRED = nameof(WEAPON_FIRED);
    public static readonly string WEAPON_DROPPED = nameof(WEAPON_DROPPED);

    public static readonly string AMMO_ADDED_TO_BATTLE = nameof(AMMO_ADDED_TO_BATTLE);
    public static readonly string AMMO_REMOVED_FROM_BATTLE = nameof(AMMO_REMOVED_FROM_BATTLE);
    public static readonly string AMMO_DESPAWN = nameof(AMMO_DESPAWN);
    public static readonly string AMMO_STATE_CHANGED = nameof(AMMO_STATE_CHANGED);
    public static readonly string AMMO_POSITION_CHANGED = nameof(AMMO_POSITION_CHANGED);

    public static readonly string LEADERBOARD_CHANGED = nameof(LEADERBOARD_CHANGED);
    public static readonly string TOTAL_PLAYER_COUNT_CHANGED = nameof(TOTAL_PLAYER_COUNT_CHANGED);
    public static readonly string ALIVE_PLAYER_COUNT_CHANGED = nameof(ALIVE_PLAYER_COUNT_CHANGED);
    public static readonly string GAME_STATE_CHANGED = nameof(GAME_STATE_CHANGED);
}
