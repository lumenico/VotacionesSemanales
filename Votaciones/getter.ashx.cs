using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Newtonsoft;
using System.Web;
using Votaciones.Core;


namespace Votaciones
{

public class getter : System.Web.IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

	public void ProcessRequest(HttpContext context)
	{
		string json = context.Request["data"];
		if (json != null) {
			object com = null;
			try {
				com = JsonConvert.DeserializeObject(json, JsonSettings.settings);
				if (com is CommandArray) {
					dynamic commandArray = (CommandArray)com;
					context.Response.Write("{");
					dynamic requiereComa = false;
					foreach (var orden_loopVariable in commandArray.Commands) {
						var orden = orden_loopVariable;
						if (requiereComa) {
							context.Response.Write(","+ orden);
						}
						requiereComa = true;
						dynamic result = GetResult(orden, JsonSettings.settings);
						context.Response.Write(result);
					}
					context.Response.Write( "}");
				} else {
					context.Response.Write(GetResult(com, JsonSettings.settings));
				}
			} catch (Exception ex) {

			}
		}
		context.Response.End();
	}
	private string GetResult(object com, JsonSerializerSettings settings)
	{
		object result = ((BaseCommand<object>)com).execute();
		string resp = null;
		if (result == null) {
			resp = "null";
		} else if (result is ResultWrapperValueAsObject) {
			dynamic casted = (ResultWrapperValueAsObject)result;
			resp = JsonConvert.SerializeObject(casted.valueAsObject, Formatting.Indented, settings);
		} else {
			resp = JsonConvert.SerializeObject(result, Formatting.Indented, settings);
		}
		return resp;
	}
	public bool IsReusable {
		get { return false; }
	}

}
}