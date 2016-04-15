
/*!
 *  @file       ObjectID.cs
 *  @brief      オブジェクトID
 *  @author     Kazuya Maruyama
 *  @date       2014/10/08
 *  @since      1.0
 *
 *  Copyright (c) 2013 2014, Kazuya Maruyama. All rights reserved.
 */
using System;

namespace game
{
    /// <summary>
    /// オブジェクトID
    /// </summary>
    public enum OBJECT_ID
    {
		STAGE,			// ステージ
		GRID,			// グリッド
		PLAYER1,		// プレイヤー1
		PLAYER2,		// プレイヤー2
		ENEMY,			// 敵管理
		BOSS,			// ボス管理
		DANMAKU,		// 弾幕
		PLAYER_BULLET,	// 自機弾管理
		ENEMY_BULLET,	// 敵(&ボス)弾管理
		EFFECT,			// エフェクト管理
		UI,				// UI
		LOGO,			// ロゴ
    }
}

