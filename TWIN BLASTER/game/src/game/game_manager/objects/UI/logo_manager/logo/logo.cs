/*!
 *  @file       logo.cs
 *  @brief      ロゴベース
 *  @author     Riki Ito
 *  @date       2015/03/19
 *  @since      1.0
 *
 *  Copyright (c) 2014 2015, Riki Ito. All rights reserved.
 */

using System;
using Sce.PlayStation.Core;

namespace game
{
	public abstract class ILogo
		:IObject
	{
		public ILogo( )
			: base( )
		{
			m_ObjectID = OBJECT_ID.LOGO;
		}
		
		/// <summary>
		/// フェードアクセッサ
		/// </summary>
		/// <value>
		/// フェード状態
		/// </value>
		public FADE FadeState
		{
			get { return m_Fade; }
		}
		
		/// <summary>
		/// フェード状態
		/// </summary>
		public enum FADE
		{
			FADE_IN,	// フェードイン
			POP,		// 表示
			FADE_OUT,	// フェードアウト
		}
		
		/// <summary>
		/// 字幕スプライト
		/// </summary>
		protected aqua.CSprite		m_Sprite;
		
		/// <summary>
		/// フェードインフェードアウトポップ時間用タイマー
		/// </summary>
		protected aqua.CFrameTimer	m_FadeTimer;
		
		/// <summary>
		/// フェード状態
		/// </summary>
		protected FADE				m_Fade;
		
		/// <summary>
		/// 位置
		/// </summary>
		protected Vector2			m_Pos;
		
		/// <summary>
		/// 数字位置
		/// </summary>
		protected Vector2			m_NPosition;
		
		/// <summary>
		/// 数字表示
		/// </summary>
		protected CNumber			m_Number;
	}
}
