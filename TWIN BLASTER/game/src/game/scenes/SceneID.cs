
/*!
 *  @file       SceneID.cs
 *  @brief      シーンID
 *  @author     Kazuya Maruyama
 *  @date       2014/10/08
 *  @since      1.0
 *
 *  Copyright (c) 2013 2014, Kazuya Maruyama. All rights reserved.
 */

namespace game
{
    /// <summary>
    /// シーンID
    /// </summary>
    public enum SCENE_ID
	{
		DUMMY		// ダミーID
		, ROOT		// ルートシーン
		, GCF_LOGO	// gdcロゴ
		, OJS_LOGO	// ojsロゴ
		, TITLE		// タイトルシーン
		, TUTORIAL	// チュートリアル
		, CREDIT	// クレジット
		, GAMEMAIN	// ゲームメインシーン
		, RESULT	// リザルトシーン(ゲームオーバー)
		
		, NUM		// シーン数
    }
}
