
/*!
 *  @file       TextureManager.cs
 *  @brief      テクスチャ管理
 *  @author     Kazuya Maruyama
 *  @date       2014/10/08
 *  @since      1.0
 *
 *  Copyright (c) 2013 2014, Kazuya Maruyama. All rights reserved.
 */

using System;
using System.Collections.Generic;

namespace game
{
    /// <summary>
    /// テクスチャID
    /// </summary>
    public enum TEXTURE_ID
    {
		WHITE,  			// 白テクスチャ
		B_GROUND,			// 背景
		GCF_LOGO,			// ゲームキャンパスフェスロゴ
		OJS_LOGO,			// 太田情報商科専門学校ロゴ

		// ここからタイトル
		TITLE_WHITE_LOGO,	// タイトル白色ロゴ
		TITLE_LOGO,			// タイトルロゴ
		START_WHITE_LOGO,	// スタート白色ロゴ
		START_LOGO,			// スタートロゴ
		TUTORIAL_WHITE_LOGO,// チュートリアル白色ロゴ
		TUTORIAL_LOGO,		// チュートリアルロゴ
		CREDIT_WHITE_LOGO,	// クレジット白色ロゴ
		CREDIT_LOGO,		// クレジットロゴ
		
		// ここからチュートリアル
		DESCRIPTION1,		// 説明画像1
		DESCRIPTION2,		// 説明画像2
		DESCRIPTION3,		// 説明画像3
		DESCRIPTION4,		// 説明画像4
		DESCRIPTION5,		// 説明画像5
		DESCRIPTION6,		// 説明画像6
		LEFT_BUTTON,		// レフトボタン
		RIGHT_BUTTON,		// ライトボタン
		
		// ここからクレジット
		CREDIT1,			// クレジット画像1
		CREDIT2,			// クレジット画像2
		
		// ここからゲームメイン
		GRID_B_GROUND,		// グリッド背景
		PLAYER1,			// 自機1   10
		PLAYER2,			// 自機2
		ENEMY1,				// 敵機1
		ENEMY2,				// 敵機2
		BOSS1,				// ボス1
		BOSS2,				// ボス2
		P_BULLET,			// 自機弾
		P_LASER,			// 自機レーザー
		E_BULLET,			// 敵機弾
		H_EFFECT,			// ヒットエフェクト
		V_EFFECT,			// バーニアエフェクト
		B_EFFECT,			// 爆発エフェクト   20
		R_EFFECT,			// 吸収エフェクト
		F_EFFECT,			// 死亡エフェクト
		S_EFFECT,			// 星エフェクト
		C_EFFECT,			// チャージエフェクト
		
		// UI
		UI_FRAME,			// UIフレーム
		BOM_ICON,			// ボムアイコン
		PLAYER_ICON,		// 自機アイコン
		P1_GAGE_FARME,		// 自機1経験値ゲージフレーム
		P1_GAGE,			// 自機1経験値ゲージ
		P2_GAGE_FARME,		// 自機2経験値ゲージフレーム	30
		P2_GAGE,			// 自機2経験値ゲージ
		P1_LV_FRAME,		// 自機1経験値レベルメモリフレーム
		P1_LV,				// 自機1経験値レベルメモリ
		P2_LV_FRAME,		// 自機2経験値レベルメモリフレーム
		P2_LV,				// 自機2経験値レベルメモリ
		MAX_LOGO,			// MAXロゴ
		SCORE_LOGO,			// スコアロゴ
		LEVEL_LOGO,			// ステージレベルロゴ
		NUMBER,				// 数字列
		LEVEL_UP_RED,		// レベルアップ赤字幕		40
		LEVEL_UP_BLUE,		// レベルアップ青字幕
		STAGE_LV,			// ステージレベル字幕
		GAMEOVER_IN,		// ゲームオーバーIN
		GAMEOVER_OUT,		// ゲームオーバーOUT
		GAMECLEAR_IN,		// ゲームクリアIN
		GAMECLEAR_OUT,		// ゲームクリアOUT
		STAGECLEAR,			// ステージクリア
		
		// ポーズ
		PAUSE_LOGO,			// ポーズロゴ
		CURSOR,				// カーソル
		
		// 休憩
		PUSH_ANY_KEY,		// push Any key
		
		// ここからリザルト
		RESULT_IN,			// リザルトIN	50
		RESULT_OUT,			// リザルトOUT
		GOOD_IN,			// グッドIN
		GOOD_OUT,			// グッドOUT
		GREAT_IN,			// グレイトIN
		GREAT_OUT,			// グレイトOUT
		PARFECT_IN,			// パーフェクトIN
		PARFECT_OUT,		// パーフェクトOUT
		
        NUM					// テクスチャ数
    }
    
    /// <summary>
    /// テクスチャ管理クラス
    /// </summary>
    public class CTextureManager
    {
        /// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <returns>
        /// インスタンス
        /// </returns>
        public static CTextureManager GetInstance( )
        {
            if( m_Instance == null )
                m_Instance = new CTextureManager( );
            
            return m_Instance;
        }
        
        /// <summary>
        /// テクスチャ読み込み
        /// </summary>
        public void Load( )
        {
            m_Textures = new aqua.CTexture[(int)TEXTURE_ID.NUM];
            
            for( int i = 0; i < (int)TEXTURE_ID.NUM; ++i )
                m_Textures[i] = new aqua.CTexture( m_texture_name[i] );
        }
        
        /// <summary>
        /// 解放
        /// </summary>
        public void Unload( )
        {
            for( int i = 0; i < (int)TEXTURE_ID.NUM; ++i )
                m_Textures[i].Unload( );
        }
        
        /// <summary>
        /// Gets the texture.
        /// </summary>
        /// <returns>
        /// The texture.
        /// </returns>
        /// <param name='id'>
        /// Identifier.
        /// </param>
        public aqua.CTexture GetTexture( TEXTURE_ID id )
        {
            return m_Textures[(int)id];
        }
        
        /// <summary>
        /// Gets the texture.
        /// </summary>
        /// <returns>
        /// The texture.
        /// </returns>
        /// <param name='id'>
        /// Identifier.
        /// </param>
        public uint GetPriority( TEXTURE_ID id )
        {
            return m_texture_priolity[(int)id];
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private CTextureManager( )
        {
        }
        
        /// <summary>
        /// インスタンス
        /// </summary>
        private static CTextureManager      m_Instance;
        
        /// <summary>
        /// テクスチャ配列
        /// </summary>
        private aqua.CTexture[]             m_Textures;
        
        /// <summary>
        /// テクスチャパス
        /// </summary>
        private static readonly string[]  m_texture_name = new string[]
        {
			// 
            "data/texture/white.png",			// 白テクスチャ
			"data/texture/background.png",		// 背景
			"data/texture/gcf_logo.png",		// GCFロゴ
			"data/texture/ota_logo.png",		// OJSロゴ

			// ここからタイトル
			"data/texture/title_logo_out.png",	// タイトル白色ロゴ
			"data/texture/title_logo_in.png",	// タイトルロゴ
			"data/texture/start_out.png",		// スタート白色ロゴ
			"data/texture/start_in.png",		// スタートロゴ
			"data/texture/help_out.png",		// チュートリアル白色ロゴ
			"data/texture/help_in.png",			// チュートリアルロゴ
			"data/texture/credit_out.png",		// クレジット白色ロゴ
			"data/texture/credit_in.png",		// クレジットロゴ
			
			// ここからチュートリアル
			"data/texture/tutorial_01.png",		// 説明画像1
			"data/texture/tutorial_02.png",		// 説明画像2
			"data/texture/tutorial_03.png",		// 説明画像3
			"data/texture/tutorial_04.png",		// 説明画像4
			"data/texture/tutorial_05.png",		// 説明画像5
			"data/texture/tutorial_06.png",		// 説明画像6
			"data/texture/left_button.png",		// レフトボタン
			"data/texture/right_button.png",	// ライトボタン
			
			// ここからクレジット
			"data/texture/credit_01.png",		// クレジット画像１
			"data/texture/credit_02.png",		// クレジット画像2
			
			// ここからゲームメイン
			"data/texture/grid_01.png",			// グリッド背景
			"data/texture/myship1.png",			// 自機1		10
			"data/texture/myship2.png",			// 自機2
			"data/texture/enemy_00.png",		// 敵機1
			"data/texture/enemy_01.png",		// 敵機2
			"data/texture/boss_00.png",			// ボス1
			"data/texture/boss_01.png",			// ボス2
			"data/texture/player_bulletA.png",	// 自機弾
			"data/texture/Laser.png",		// 自機レーザー
			"data/texture/enemy_bulletB.png",	// 敵機弾
			"data/texture/particleE.png",		// ヒットエフェクト
			"data/texture/particleA.png",		// バーニアエフェクト
			"data/texture/bomb_effect.png",		// 爆発エフェクト
			"data/texture/repel_effect.png",	// 吸収エフェクト
			"data/texture/fflower_rainbow.png",	// 死亡エフェクト
			"data/texture/particleS.png",		// 星エフェクト
			"data/texture/chargeEffect.png",	// チャージエフェクト
			
			// UI
			"data/texture/ui_frame.png",		// UIフレーム
			"data/texture/bom_icon.png",		// ボムアイコン
			"data/texture/player_icon.png",		// 自機アイコン
			"data/texture/gage1_out.png",		// 自機1経験値ゲージフレーム
			"data/texture/gage1_in.png",		// 自機1経験値ゲージ
			"data/texture/gage2_out.png",		// 自機2経験値ゲージフレーム
			"data/texture/gage2_in.png",		// 自機2経験値ゲージ			30
			"data/texture/memory1_out.png",		// 自機1経験値レベルメモリフレーム
			"data/texture/memory1_in.png",		// 自機1経験値レベルメモリ
			"data/texture/memory2_out.png",		// 自機2経験値レベルメモリフレーム
			"data/texture/memory2_in.png",		// 自機2経験値レベルメモリ
			"data/texture/max_logo_01.png",		// MAXロゴ
			"data/texture/score_in.png",		// スコアロゴ
			"data/texture/stage.png",			// ステージロゴ
			"data/texture/number_00.png",		// 数字
			"data/texture/levelup_red.png",		// レベルアップ赤字幕
			"data/texture/levelup_blue.png",	// レベルアップ青字幕
			"data/texture/level_up.png",		// ステージレベル字幕
			"data/texture/gameover.png",		// ゲームオーバー
			"data/texture/gameover_out.png",	// ゲームオーバーOUT
			"data/texture/gameclear.png",		// ゲームクリア
			"data/texture/gameclear_out.png",	// ゲームクリアOUT
			"data/texture/stage_clear.png",		// ステージクリア
			
			// ポーズ
			"data/texture/pause.png",			// ポーズロゴ
			"data/texture/cursor.png",			// カーソル
			
			// 休憩
			"data/texture/nextstage.png",		// push Any key
			
			// ここからリザルト
			"data/texture/result.png",			// リザルトIN
			"data/texture/result_out.png",		// リザルトOUT
			"data/texture/good.png",			// グッドIN
			"data/texture/good_out.png",		// グッドOUT			50
			"data/texture/great.png",			// グレイトIN
			"data/texture/greet_out.png",		// グレイトOUT
			"data/texture/parfect.png",			// パーフェクトIN
			"data/texture/parfect_out.png",		// パーフェクトOUT	54
		};
		
        /// <summary>
        /// テクスチャプライオリティ
        /// </summary>
        private static readonly uint[]  m_texture_priolity = new uint[]
        {
			// 各シーン
			10000,		// 白テクスチャ
			1,			// 背景
			2,			// GCFロゴ
			3,			// OJSロゴ
			
			// ここからタイトル
			5,			// タイトル白色ロゴ
			6,			// タイトルロゴ
			7,			// スタート白色ロゴ
			8,			// スタートロゴ
			9,			// チュートリアル白色ロゴ
			10,			// チュートリアルロゴ
			11,			// クレジット白色ロゴ
			12,			// クレジットロゴ
			
			// ここからチュートリアル
			20,			// 説明画像1
			19,			// 説明画像2
			18,			// 説明画像3
			17,			// 説明画像4
			16,			// 説明画像5
			15,			// 説明画像6
			25,			// レフトボタン
			26,			// ライトボタン
			
			// ここからクレジット
			24,			// クレジット画像1
			23,			// クレジット画像2
			
			// ここからゲームメイン
			30,			// グリッド背景
			100,		// 自機1
			101,		// 自機2
			31,			// 敵機1
			32,			// 敵機2
			33,			// ボス1
			34,			// ボス2
			35,			// 自機弾
			1000,		// 自機レーザー
			36,			// 敵機弾
			37,			// ヒットエフェクト
			38,			// バーニアエフェクト
			39,			// 爆発エフェクト
			40,			// 吸収エフェクト
			41,			// 死亡エフェクト
			42,			// 星エフェクト
			43,			// チャージエフェクト
			
			// UI
			51,			// UIフレーム
			52,			// ボムアイコン
			53,			// 自機アイコン
			54,			// 自機1経験値ゲージフレーム
			55,			// 自機1経験値ゲージ
			56,			// 自機2経験値ゲージフレーム
			57,			// 自機2経験値ゲージ
			58,			// 自機1経験値レベルメモリフレーム
			59,			// 自機1経験値レベルメモリ
			60,			// 自機2経験値レベルメモリフレーム
			61,			// 自機2経験値レベルメモリ
			62,			// MAXロゴ
			63,			// スコアロゴ
			64,			// ステージレベルロゴ
			65,			// 数字
			66,			// レベルアップ赤字幕
			67,			// レベルアップ青字幕
			68,			// ステージレベル字幕
			200,		// ゲームオーバーIN
			201,		// ゲームオーバーOUT
			203,		// ゲームクリアIN
			204,		// ゲームクリアOUT
			202,		// ステージクリア
			
			// ポーズ
			10002,		// ポーズロゴ
			10003,		// カーソル
			
			// 休憩
			10001,		// pushAnykey
			
			// ここからリザルト
			80,			// リザルトIN
			81,			// リザルトOUT
			82,			// グッドIN
			83,			// グッドOUT
			84,			// グレイトIN
			85,			// グレイトOUT
			86,			// パーフェクトIN
			87,			// パーフェクトOUT
        };
    }
}
