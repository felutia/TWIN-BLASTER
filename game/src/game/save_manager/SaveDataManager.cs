
/*!
 *  @file       SaveDataManager.cs
 *  @brief      セーブデータ管理
 *  @author     Kazuya Maruyama
 *  @date       2014/10/09
 *  @since      1.0
 *
 *  Copyright (c) 2013 2014, Kazuya Maruyama. All rights reserved.
 */

namespace game
{
    /// <summary>
    /// セーブデータ管理クラス
    /// </summary>
    public sealed class CSaveDataManager
    {
        /// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <returns>
        /// 自分自身のインスタンス
        /// </returns>
        public static CSaveDataManager  GetInstance( )
        {
            if( m_Instance == null )
                m_Instance = new CSaveDataManager( );
            
            return m_Instance;
        }
        
        /// <summary>
        /// セーブデータの読み込み
        /// </summary>
        public void Read( )
        {
            // セーブデータが存在しない場合、新規作成
            // 存在する場合読み込む
            if( aqua.CSaveData.Exists( ) == false )
            {
                // 新規データ作成
                m_SaveData = new CSaveData( );
                
                // 新規データの書き出し
                Write( );
            }
            else
            {
                // セーブデータ読み込み
                m_SaveData = (CSaveData)aqua.CSaveData.Read( );
                
                // 各必要なパラメータに受け渡す
                
            }
        }
        
        /// <summary>
        /// セーブデータの書き出し
        /// </summary>
        public void Write( )
        {
            // 各必要なパラメータからデータを引き出す
            
            
            // 書き出し
            aqua.CSaveData.Write( m_SaveData );
        }
        
        /// <summary>
        /// セーブデータリセット
        /// </summary>
        public void Reset( )
        {
			// リセット内容
			m_SaveData.first = false;
        }
        
        /// <summary>
        /// セーブデータの取得と設定
        /// </summary>
        /// <value>
        /// セーブデータ
        /// </value>
        public CSaveData SaveData
        {
            get { return m_SaveData;  }
            set { m_SaveData = value; }
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private CSaveDataManager( )
        {
            m_SaveData = new CSaveData( );
        }
        
        /// <summary>
        /// インスタンス
        /// </summary>
        private static CSaveDataManager m_Instance;
        
        /// <summary>
        /// セーブデータ
        /// </summary>
        private CSaveData               m_SaveData;
    }
}
