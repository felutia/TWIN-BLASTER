/*!
 *  @file       laser_effect.cs
 *  @brief      レーザーエフェクト
 *  @author     Riki Ito
 *  @date       2015/03/19
 *  @since      1.0
 *
 *  Copyright (c) 2014 2015, Riki Ito. All rights reserved.
 */

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Input;
using System;
using System.Collections.Generic;

namespace game
{
	/// <summary>
	/// レーザークラス
	/// </summary>
	public class CLaserEffect
		: IEffect
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CLaserEffect( COLOR_ID color, Vector2 pos )
			: base( color, pos )
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize ()
		{
			m_EffectID = EFFECT_ID.LASER_EFFECT;
			
			// スプライト生成
			m_Sprite = CSpriteManager.Create( TEXTURE_ID.P_LASER );
			
			// 色設定
			if( m_Color == COLOR_ID.BLUE )
				m_Sprite.Color = new Vector4( 0.6f, 0.6f, 1.0f, 0.7f );
			else if( m_Color == COLOR_ID.RAINBOW )
            	m_Sprite.Color = CColorManager.GetColor( );
			else if ( m_Color == COLOR_ID.RED )
				m_Sprite.Color = new Vector4( 1.0f, 0.3f, 0.3f, 0.7f );
			else if( m_Color == COLOR_ID.WHITE )
				m_Sprite.Color = new Vector4( 1.0f, 1.0f, 1.0f, 0.7f );
			
			// サイズ設定
			m_Size = new aqua.CSize( 960.0f, 120.0f );
			
			// 中心値設定
			m_Sprite.Center = new Vector2( 480.0f, 60.0f );
			
			// 位置設定
			m_Sprite.Position = m_Pos;
			
			m_AnimaFrame = 0;
			
			// アニメーションタイマー設定
			m_AnimaTimer = new aqua.CFrameTimer( 3 );
			
			m_MasupaTimer = new aqua.CFrameTimer( 10 );
			
			// アニメーション用RECT設定
			aqua.CRect rect = new aqua.CRect( 0.0f, 0.0f, 0.0f, 0.0f );
			
			m_Sprite.Scaling = new Vector2( 1.0f, 2.0f );
			
			rect.left	= 0;
			rect.top	= ( m_AnimaFrame % 10 ) * m_Size.height;
			rect.right	= rect.left + m_Size.width;
			rect.bottom	= rect.top + m_Size.height;
			
			m_Sprite.Rect = rect;
			
			// 4地点宣言
			m_LaserPoint4 = new Vector2[m_point_num];
			m_Container4 = new Vector2[m_point_num];
			
			for( int i = 0; i < m_point_num; i++ )
			{
				m_LaserPoint4[i] = new Vector2( 0.0f, 0.0f );
				m_Container4[i] = m_LaserPoint4[i];
			}
//			
//			m_POINT = CSpriteManager.Create( TEXTURE_ID.EXIT_LOGO );
//			m_POINT.Position = m_LaserPoint4[0];
//			m_POINT.Visible = false;
//			aqua.CRect recte = new aqua.CRect( 0.0f, 0.0f, 0.0f, 0.0f );
//	
//			recte.left	= 0;
//			recte.top	= 0;
//			recte.right	= m_Size.width;
//			recte.bottom	= m_Size.height;
//			
//			m_POINT.Rect = recte;
//			
			CUnit.GetInstance( ).Bom -= 2;
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public override void Update( )
		{
			m_AnimaTimer.Update( );
			m_MasupaTimer.Update( );
			
			CPlayer1 player1 = (CPlayer1)CGameManager.GetInstance( ).FindObject( OBJECT_ID.PLAYER1 );
			CPlayer2 player2 = (CPlayer2)CGameManager.GetInstance( ).FindObject( OBJECT_ID.PLAYER2 );
			
			if( m_Color == COLOR_ID.RED )
			{
				if( player1 == null )
				{
					m_IsActive = false;
					
					return;
				}
				
				m_Sprite.Position = player1.Position + player1.Center + new Vector2( 5.0f, -m_Size.height );
			}
			else if( m_Color == COLOR_ID.BLUE )
			{
				if( player2 == null )
				{
					m_IsActive = false;
					
					return;
				}
				
				m_Sprite.Position = player2.Position + player2.Center + new Vector2( 5.0f, -m_Size.height );
			}
			
			if( m_AnimaTimer.IsEnd( ) )
			{
				++m_AnimaFrame;
				
				if( m_AnimaFrame == 3 )
				{
					m_AnimaTimer = new aqua.CFrameTimer( 12 );
				}
				else if( m_AnimaFrame == 5 )
				{
					m_AnimaTimer = new aqua.CFrameTimer( 60 );
				}
				else if( m_AnimaFrame == 6 )
				{
					m_AnimaTimer = new aqua.CFrameTimer( 12 );
				}
				else if( m_AnimaFrame == 7 )
				{
					m_AnimaTimer = new aqua.CFrameTimer( 3 );
				}
				// アニメーション終了
				else if( m_AnimaFrame > 10 )
				{
					// オブジェクト削除
					m_IsActive = false;
					
					Vector2 p1v = player1.Position + player1.Center;
					Vector2 p2v = player2.Position + player2.Center;
					
					Vector2 pv = p1v - p2v;
					
					// 距離算出
					float len = pv.Length( );
					
					// ツインブラスターフラグON
					player1.TwinBlaster = true;
					player2.TwinBlaster = true;
//					
//					m_POINT.Visible = false;
				}
				
				// アニメーション用RECT設定
				aqua.CRect rect = new aqua.CRect( 0.0f, 0.0f, 0.0f, 0.0f );
				
				rect.left	= 0;
				rect.top	= ( m_AnimaFrame % 10 ) * m_Size.height;
				rect.right	= rect.left + m_Size.width;
				rect.bottom	= rect.top + m_Size.height;
				
				m_Sprite.Rect = rect;
			}
			
			if( m_MasupaTimer.IsEnd( ) == true )
			{
				// ランダム生成
				int x    = aqua.CRandom.GetInstance( ).GetValue( 900 - 32 )  + 32;	// X座標
				int y    = aqua.CRandom.GetInstance( ).GetValue( 544 - 172 ) + 32;	// Y座標
				Vector2 create = new Vector2( x, y );
				
				CEffectManager.GetInstance( ).Create( EFFECT_ID.MASUPA_EFFECT, OBJECT_ID.EFFECT, COLOR_ID.RAINBOW, create );
			}
			
			if( m_AnimaFrame == 3 )
			{
				m_Container4[0] = m_Sprite.Position + new Vector2( 25.0f, m_Size.height / 3 * 2 );
				m_Container4[1] = m_Sprite.Position + new Vector2( 25.0f, -m_Size.height / 3 * 2 );
				m_Container4[2] = m_Sprite.Position + new Vector2( m_Size.width + 25.0f, -m_Size.height / 3 * 2 );
				m_Container4[3] = m_Sprite.Position + new Vector2( m_Size.width + 25.0f, m_Size.height / 3 * 2 );
//				
//				m_POINT.Visible = true;
//				m_POINT.Position = m_LaserPoint4[0];
//				if( m_Color == COLOR_ID.BLUE )
//					m_POINT.Visible = false;
//				
//				aqua.CRect recte = new aqua.CRect( 0.0f, 0.0f, 0.0f, 0.0f );
//				
//				recte.left	= 0;
//				recte.top	= 0;
//				recte.right	= m_Size.width;
//				recte.bottom= m_Size.height - m_Size.height / 3;
//				
//				m_POINT.Rect = recte;
			}
			else if( m_AnimaFrame == 5 )
			{
				m_Container4[0] = m_Sprite.Position + new Vector2( 25.0f, m_Size.height / 2 );
				m_Container4[1] = m_Sprite.Position + new Vector2( 25.0f, -m_Size.height / 2 );
				m_Container4[2] = m_Sprite.Position + new Vector2( m_Size.width + 25.0f, -m_Size.height / 2 );
				m_Container4[3] = m_Sprite.Position + new Vector2( m_Size.width + 25.0f, m_Size.height / 2 );
//				
//				aqua.CRect recte = new aqua.CRect( 0.0f, 0.0f, 0.0f, 0.0f );
//				
//				recte.left	= 0;
//				recte.top	= 0;
//				recte.right	= m_Size.width;
//				recte.bottom= m_Size.height;
//				
//				m_POINT.Rect = recte;
			}
			else if( m_AnimaFrame == 7 )
			{
				m_Container4[0] = m_Sprite.Position + new Vector2( 25.0f, m_Size.height / 3 * 2 );
				m_Container4[1] = m_Sprite.Position + new Vector2( 25.0f, -m_Size.height / 3 * 2 );
				m_Container4[2] = m_Sprite.Position + new Vector2( m_Size.width + 25.0f, -m_Size.height / 3 * 2 );
				m_Container4[3] = m_Sprite.Position + new Vector2( m_Size.width + 25.0f, m_Size.height / 3 * 2 );
//				
//				aqua.CRect recte = new aqua.CRect( 0.0f, 0.0f, 0.0f, 0.0f );
//				
//				recte.left	= 0;
//				recte.top	= 0;
//				recte.right	= m_Size.width;
//				recte.bottom= m_Size.height - m_Size.height / 3;
//				
//				m_POINT.Rect = recte;
			}
//			else if( m_AnimaFrame == 8 )
//				m_POINT.Visible = false;
			
			for( int i = 0; i < m_point_num; i++ )
			{
				m_LaserPoint4[i] = m_Container4[i];
			}
			
			LaserToEnemyBoss( );
//			
//			m_POINT.Position = m_LaserPoint4[0];
		}
		
		/// <summary>
		/// レーザーと敵とボスの当たり判定
		/// </summary>
		public void LaserToEnemyBoss( )
		{
			List<IObject> list = CGameManager.GetInstance().ObjectList;
			
			if( m_LaserPoint4[0] == new Vector2( 0.0f, 0.0f ) && m_LaserPoint4[2] == new Vector2( 0.0f, 0.0f ) )
				return;
			
			for ( int i = 0; i < list.Count; ++i)
			{
				// 敵と比較
				if( list[i].ObjectID == OBJECT_ID.ENEMY )
				{
					IEnemy enemy = (IEnemy)list[i];
					
					// 敵のサイズ取得
					aqua.CSize size = new aqua.CSize( );
					
					size.width  = enemy.Size.width  * 0.5f;
					size.height = enemy.Size.height * 0.5f;
					
					// 敵中心点
					Vector2 ev = new Vector2( enemy.Sprite.Position.X + size.width, enemy.Sprite.Position.Y + size.height );
					
					// 長方形の頂点との当たり判定
					for( int k = 0; k < m_point_num; k++ )
					{
						Vector2 m4v = m_LaserPoint4[k];
						
						
						Vector2 v = m4v - ev;
						
						// 距離算出
						float len = v.Length( );
						
						// 敵半径内かどうか判定
						if( len < size.width )
						{
							// ダメージ
							enemy.BeShot( 5 );
							
							break;
						}
					}
					
					// 長方形の辺との当たり判定
					// 点間ベクトル
					Vector2 v0to1 = new Vector2( m_LaserPoint4[1].X - m_LaserPoint4[0].X, m_LaserPoint4[1].Y - m_LaserPoint4[0].Y );
					Vector2 v1to2 = new Vector2( m_LaserPoint4[2].X - m_LaserPoint4[1].X, m_LaserPoint4[2].Y - m_LaserPoint4[1].Y );
					Vector2 v2to3 = new Vector2( m_LaserPoint4[3].X - m_LaserPoint4[2].X, m_LaserPoint4[3].Y - m_LaserPoint4[2].Y );
					Vector2 v3to0 = new Vector2( m_LaserPoint4[0].X - m_LaserPoint4[3].X, m_LaserPoint4[0].Y - m_LaserPoint4[3].Y );
					
					// 敵の中心点と点のベクトル
					Vector2 v0toE = new Vector2( ev.X - m_LaserPoint4[0].X, ev.Y - m_LaserPoint4[0].Y );
					Vector2 v1toE = new Vector2( ev.X - m_LaserPoint4[1].X, ev.Y - m_LaserPoint4[1].Y );
					Vector2 v2toE = new Vector2( ev.X - m_LaserPoint4[2].X, ev.Y - m_LaserPoint4[2].Y );
					Vector2 v3toE = new Vector2( ev.X - m_LaserPoint4[3].X, ev.Y - m_LaserPoint4[3].Y );
					
					// ベクトルの外積 AP_X＊AB_Y – AB_X＊AP_Y
					float out0Eto01 = ( v0toE.X * v0to1.Y ) - ( v0to1.X * v0toE.Y );
					float out1Eto12 = ( v1toE.X * v1to2.Y ) - ( v1to2.X * v1toE.Y );
					float out3Eto23 = ( v2toE.X * v2to3.Y ) - ( v2to3.X * v2toE.Y );
					float out2Eto30 = ( v3toE.X * v3to0.Y ) - ( v3to0.X * v3toE.Y );
					
					// ベクトルの内積 AP_X＊AB_X ＋ AP_Y＊AB_Y
					float in0Eto01  = ( v0toE.X * v0to1.X ) + ( v0toE.Y * v0to1.Y );
					float in1Eto12  = ( v1toE.X * v1to2.X ) + ( v1toE.Y * v1to2.Y );
					float in3Eto23  = ( v2toE.X * v2to3.X ) + ( v2toE.Y * v2to3.Y );
					float in2Eto30  = ( v3toE.X * v3to0.X ) + ( v3toE.Y * v3to0.Y );
					
					// 敵中心地点とのベクトルの距離算出
					float len0toE = v0toE.Length( );
					float len1toE = v1toE.Length( );
					float len3toE = v3toE.Length( );
					float len2toE = v2toE.Length( );
					
					// 0to1の線と敵中心点との垂線
					float Drop0to1 = Math.Abs( out0Eto01 ) / v0to1.Length( );
					// 1to2の線と敵中心点との垂線
					float Drop1to2 = Math.Abs( out1Eto12 ) / v1to2.Length( );
					// 2to3の線と敵中心点との垂線
					float Drop2to3 = Math.Abs( out3Eto23 ) / v2to3.Length( );
					// 3to0の線と敵中心点との垂線
					float Drop3to0 = Math.Abs( out2Eto30 ) / v3to0.Length( );
					
					// 垂線が敵半径以下の時当たり判定あり
					if( Drop0to1 <= size.width )
					{
						// ダメージ
						enemy.BeShot( 5 );
						
						break;
					}
					
					if( Drop1to2 <= size.width )
					{
						// ダメージ
						enemy.BeShot( 5 );
						
						break;
					}
					
					if( Drop2to3 <= size.width )
					{
						// ダメージ
						enemy.BeShot( 5 );
						
						break;
					}
					
					if( Drop3to0 <= size.width )
					{
						// ダメージ
						enemy.BeShot( 5 );
						
						break;
					}
					
					// 垂線よりも敵中心位置とのベクトルのほうが近い場合(内積使用)
					if( in0Eto01 < 0 )
					{
						if( len0toE <= size.width )
						{
							// ダメージ
							enemy.BeShot( 5 );
							
							break;
						}
					}
					
					if( in1Eto12 < 0 )
					{
						if( len1toE <= size.width )
						{
							// ダメージ
							enemy.BeShot( 5 );
							
							break;
						}
					}
					
					if( in3Eto23 < 0 )
					{
						if( len2toE <= size.width )
						{
							// ダメージ
							enemy.BeShot( 5 );
							
							break;
						}
					}
					
					if( in2Eto30 < 0 )
					{
						if( len3toE <= size.width )
						{
							// ダメージ
							enemy.BeShot( 5 );
							
							break;
						}
					}
					
					// レーザの当たり判定内に円がある場合
					// 外積 / 内積 = 逆正接のθ算出
					float tan0for1 = FMath.Atan2( out0Eto01, in0Eto01 );
					float tan1for2 = FMath.Atan2( out1Eto12, in1Eto12 );
					float tan2for3 = FMath.Atan2( out2Eto30, in2Eto30 );
					float tan3for0 = FMath.Atan2( out3Eto23, in3Eto23 );
					
					// Degree化
					float rad0for1 = FMath.Degrees( tan0for1 );
					float rad1for2 = FMath.Degrees( tan1for2 );
					float rad2for3 = FMath.Degrees( tan2for3 );
					float rad3for0 = FMath.Degrees( tan3for0 );
					
					if( 0 <= rad0for1 && rad0for1 <= 90 &&
						0 <= rad1for2 && rad1for2 <= 90 &&
						0 <= rad2for3 && rad2for3 <= 90 &&
						0 <= rad3for0 && rad3for0 <= 90 )
					{
						// ダメージ
						enemy.BeShot( 5 );
						
						break;
					}
				}
				
				// ボスと比較
				if( list[i].ObjectID == OBJECT_ID.BOSS )
				{
					IBoss boss = (IBoss)list[i];
					
					// ボスのサイズ取得
					aqua.CSize size = new aqua.CSize( );
					
					size.width  = boss.Size.width  * 0.5f;
					size.height = boss.Size.height * 0.5f;
					
					// 敵中心点
					Vector2 bv = new Vector2( boss.Sprite.Position.X + size.width, boss.Sprite.Position.Y + size.height );
					
					// 長方形の頂点との当たり判定
					for( int k = 0; k < m_point_num; k++ )
					{
						Vector2 m4v = m_LaserPoint4[k];
						
						
						Vector2 v = m4v - bv;
						
						// 距離算出
						float len = v.Length( );
						
						// 敵半径内かどうか判定
						if( len < size.width )
						{
							// ダメージ
							boss.BeShot( 3 );
							
							break;
						}
					}
					
					// 長方形の辺との当たり判定
					// 点間ベクトル
					Vector2 v0to1 = new Vector2( m_LaserPoint4[1].X - m_LaserPoint4[0].X, m_LaserPoint4[1].Y - m_LaserPoint4[0].Y );
					Vector2 v1to2 = new Vector2( m_LaserPoint4[2].X - m_LaserPoint4[1].X, m_LaserPoint4[2].Y - m_LaserPoint4[1].Y );
					Vector2 v2to3 = new Vector2( m_LaserPoint4[3].X - m_LaserPoint4[2].X, m_LaserPoint4[3].Y - m_LaserPoint4[2].Y );
					Vector2 v3to0 = new Vector2( m_LaserPoint4[0].X - m_LaserPoint4[3].X, m_LaserPoint4[0].Y - m_LaserPoint4[3].Y );
					
					// ボスの中心点と点のベクトル
					Vector2 v0toE = new Vector2( bv.X - m_LaserPoint4[0].X, bv.Y - m_LaserPoint4[0].Y );
					Vector2 v1toE = new Vector2( bv.X - m_LaserPoint4[1].X, bv.Y - m_LaserPoint4[1].Y );
					Vector2 v2toE = new Vector2( bv.X - m_LaserPoint4[2].X, bv.Y - m_LaserPoint4[2].Y );
					Vector2 v3toE = new Vector2( bv.X - m_LaserPoint4[3].X, bv.Y - m_LaserPoint4[3].Y );
					
					// ベクトルの外積 AP_X＊AB_Y – AB_X＊AP_Y
					float out0Eto01 = ( v0toE.X * v0to1.Y ) - ( v0to1.X * v0toE.Y );
					float out1Eto12 = ( v1toE.X * v1to2.Y ) - ( v1to2.X * v1toE.Y );
					float out3Eto23 = ( v2toE.X * v2to3.Y ) - ( v2to3.X * v2toE.Y );
					float out2Eto30 = ( v3toE.X * v3to0.Y ) - ( v3to0.X * v3toE.Y );
					
					// ベクトルの内積 AP_X＊AB_X ＋ AP_Y＊AB_Y
					float in0Eto01  = ( v0toE.X * v0to1.X ) + ( v0toE.Y * v0to1.Y );
					float in1Eto12  = ( v1toE.X * v1to2.X ) + ( v1toE.Y * v1to2.Y );
					float in3Eto23  = ( v2toE.X * v2to3.X ) + ( v2toE.Y * v2to3.Y );
					float in2Eto30  = ( v3toE.X * v3to0.X ) + ( v3toE.Y * v3to0.Y );
					
					// 敵中心地点とのベクトルの距離算出
					float len0toE = v0toE.Length( );
					float len1toE = v1toE.Length( );
					float len3toE = v3toE.Length( );
					float len2toE = v2toE.Length( );
					
					// 0to1の線と敵中心点との垂線
					float Drop0to1 = Math.Abs( out0Eto01 ) / v0to1.Length( );
					// 1to2の線と敵中心点との垂線
					float Drop1to2 = Math.Abs( out1Eto12 ) / v1to2.Length( );
					// 2to3の線と敵中心点との垂線
					float Drop2to3 = Math.Abs( out3Eto23 ) / v2to3.Length( );
					// 3to0の線と敵中心点との垂線
					float Drop3to0 = Math.Abs( out2Eto30 ) / v3to0.Length( );
					
					// 垂線が敵半径以下の時当たり判定あり
					if( Drop0to1 <= size.width )
					{
						// ダメージ
						boss.BeShot( 3 );
						
						break;
					}
					
					if( Drop1to2 <= size.width )
					{
						// ダメージ
						boss.BeShot( 3 );
						
						break;
					}
					
					if( Drop2to3 <= size.width )
					{
						// ダメージ
						boss.BeShot( 3 );
						
						break;
					}
					
					if( Drop3to0 <= size.width )
					{
						// ダメージ
						boss.BeShot( 3 );
						
						break;
					}
					
					// 垂線よりも敵中心位置とのベクトルのほうが近い場合(内積使用)
					if( in0Eto01 < 0 )
					{
						if( len0toE <= size.width )
						{
							// ダメージ
							boss.BeShot( 3 );
							
							break;
						}
					}
					
					if( in1Eto12 < 0 )
					{
						if( len1toE <= size.width )
						{
							// ダメージ
							boss.BeShot( 3 );
							
							break;
						}
					}
					
					if( in3Eto23 < 0 )
					{
						if( len2toE <= size.width )
						{
							// ダメージ
							boss.BeShot( 3 );
							
							break;
						}
					}
					
					if( in2Eto30 < 0 )
					{
						if( len3toE <= size.width )
						{
							// ダメージ
							boss.BeShot( 3 );
							
							break;
						}
					}
					
					// レーザの当たり判定内に円がある場合
					// 外積 / 内積 = 逆正接のθ算出
					float tan0for1 = FMath.Atan2( out0Eto01, in0Eto01 );
					float tan1for2 = FMath.Atan2( out1Eto12, in1Eto12 );
					float tan2for3 = FMath.Atan2( out2Eto30, in2Eto30 );
					float tan3for0 = FMath.Atan2( out3Eto23, in3Eto23 );
					
					// Degree化
					float rad0for1 = FMath.Degrees( tan0for1 );
					float rad1for2 = FMath.Degrees( tan1for2 );
					float rad2for3 = FMath.Degrees( tan2for3 );
					float rad3for0 = FMath.Degrees( tan3for0 );
					
					if( 0 <= rad0for1 && rad0for1 <= 90 &&
						0 <= rad1for2 && rad1for2 <= 90 &&
						0 <= rad2for3 && rad2for3 <= 90 &&
						0 <= rad3for0 && rad3for0 <= 90 )
					{
						// ダメージ
						boss.BeShot( 3 );
						
						break;
					}
				}
				
				// 敵機弾と比較
				if( list[i].ObjectID == OBJECT_ID.ENEMY_BULLET )
				{
					for( ; ; )
					{
						IEnemyBullet enemybullet = (IEnemyBullet)list[i];
						
						// 弾解放
						enemybullet.Active = false;
						
						CEffectManager.GetInstance( ).Create( EFFECT_ID.HIT_EFFECT, OBJECT_ID.EFFECT, COLOR_ID.RAINBOW, enemybullet.GetPos );
						
						break;
					}
				}
			}
		}
		
		/// <summary>
		/// 地点数
		/// </summary>
		private int					m_point_num = 4;
		
		/// <summary>
		/// 長方形4地点
		/// </summary>
		private Vector2[]			m_LaserPoint4;
		
		/// <summary>
		/// 4地点を入れる入れ物
		/// </summary>
		private Vector2[]			m_Container4;
		
		/// <summary>
		/// マスパ用タイマー
		/// </summary>
		private aqua.CFrameTimer	m_MasupaTimer;
//		
//		private aqua.CSprite		m_POINT;
    }
}
