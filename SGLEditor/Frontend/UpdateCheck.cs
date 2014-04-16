using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace SGLEditor {

	class UpdateCheck {
		const int currentVersion = 101002;
		private SimpleSGLEditor editor;

		public UpdateCheck(SimpleSGLEditor editor) {
			this.editor = editor;
		}

		public void DoWork() {

			String uri = @"http://moonshadow.hostbeef.com/sglerror/update.php";
			String updateRaw = HttpPost(uri, "");
			if (updateRaw != null) {
				String[] update = updateRaw.Split('|');
				if (update.Length == 2) {
					if (Int32.Parse(update[0]) > UpdateCheck.currentVersion) {
						// Update avaliable
						editor.ShowUpdateAvaliable(editor, update[1]);
					}

				}
			}

		}

		private string HttpPost(string uri, string parameters) {
			// parameters: name1=value1&name2=value2	
			WebRequest webRequest = WebRequest.Create(uri);
			//string ProxyString = 
			//   System.Configuration.ConfigurationManager.AppSettings
			//   [GetConfigKey("proxy")];
			//webRequest.Proxy = new WebProxy (ProxyString, true);
			//Commenting out above required change to App.Config
			webRequest.ContentType = "application/x-www-form-urlencoded";
			webRequest.Method = "POST";
			byte[] bytes = Encoding.ASCII.GetBytes(parameters);
			Stream os = null;
			try { // send the Post
				webRequest.ContentLength = bytes.Length;   //Count bytes to send
				os = webRequest.GetRequestStream();
				os.Write(bytes, 0, bytes.Length);         //Send it
			} catch (WebException ex) {
				Console.WriteLine("HttpPost: Request error");
			} finally {
				if (os != null) {
					os.Close();
				}
			}

			try { // get the response
				WebResponse webResponse = webRequest.GetResponse();
				if (webResponse == null) { return null; }
				StreamReader sr = new StreamReader(webResponse.GetResponseStream());
				return sr.ReadToEnd().Trim();
			} catch (WebException ex) {
				Console.WriteLine("HttpPost: Response error");
			}
			return null;
		} // end HttpPost

	}


}
