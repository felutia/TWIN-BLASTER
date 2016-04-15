/*!
 *  @file       effect_ID.cs
 *  @brief      エフェクトID
 *  @author     Riki Ito
 *  @date       2015/03/19
 *  @since      1.0
 *
 *  Copyright (c) 2014 2015, Riki Ito. All rights reserved.
 */

using System;

namespace game
{
	public enum EFFECT_ID
	{
		HIT_EFFECT,			// ヒットエフェクト
		VERNIER_EFFECT,		// バーニアエフェクト
		BOMB_EFFECT,		// 爆発エフェクト
		REPEL_EFFECT,		// ハジキエフェクト
		MASUPA_EFFECT,		// 自機弾ボムエフェクト
		DEAD_EFFECT,		// 死亡エフェクト
		RESONANT_EFFECT,	// 共鳴エフェクト
		LASER_EFFECT,		// レーザー
		CHARGE_EFFECT,		// チャージエフェクト
	}
}