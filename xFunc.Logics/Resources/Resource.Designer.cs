﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace xFunc.Logics.Resources {
    using System;
    
    
    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは StronglyTypedResourceBuilder クラスが ResGen
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバーを追加または削除するには、.ResX ファイルを編集して、/str オプションと共に
    // ResGen を実行し直すか、または VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   このクラスで使用されているキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("xFunc.Logics.Resources.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   厳密に型指定されたこのリソース クラスを使用して、すべての検索リソースに対し、
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   ツリーの構文解析中にエラーが発生しました。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string ErrorWhileParsingTree {
            get {
                return ResourceManager.GetString("ErrorWhileParsingTree", resourceCulture);
            }
        }
        
        /// <summary>
        ///   式が無効です。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string InvalidExpression {
            get {
                return ResourceManager.GetString("InvalidExpression", resourceCulture);
            }
        }
        
        /// <summary>
        ///   式が不正です。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string InvalidInFirst {
            get {
                return ResourceManager.GetString("InvalidInFirst", resourceCulture);
            }
        }
        
        /// <summary>
        ///   関数に無効な変数があります。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string InvalidNumberOfVariables {
            get {
                return ResourceManager.GetString("InvalidNumberOfVariables", resourceCulture);
            }
        }
        
        /// <summary>
        ///   括弧が閉じられていません。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string NotBalanced {
            get {
                return ResourceManager.GetString("NotBalanced", resourceCulture);
            }
        }
        
        /// <summary>
        ///   関数が定義されていません。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string NotSpecifiedFunction {
            get {
                return ResourceManager.GetString("NotSpecifiedFunction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   &apos;{0}&apos;はサポートされていません。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string NotSupportedSymbol {
            get {
                return ResourceManager.GetString("NotSupportedSymbol", resourceCulture);
            }
        }
        
        /// <summary>
        ///   サポートされていない型です。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string NotSupportedToken {
            get {
                return ResourceManager.GetString("NotSupportedToken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   予期しないエラーです。 に類似しているローカライズされた文字列を検索します。
        /// </summary>
        internal static string UnexpectedError {
            get {
                return ResourceManager.GetString("UnexpectedError", resourceCulture);
            }
        }
    }
}
