using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Defective.JSON;

namespace GeoJSON {

	public class GeoJSONObject {

		public string type;

		public GeoJSONObject() {
		}

		public GeoJSONObject(JSONObject jsonObject) {
			if(jsonObject != null)
				type = jsonObject ["type"].stringValue;
		}

		//Will always return a FeatureCollection...
		static public FeatureCollection Deserialize(string encodedString) {
			FeatureCollection collection;

			JSONObject jsonObject = new JSONObject (encodedString);
			if (jsonObject ["type"].stringValue == "FeatureCollection") {
				collection = new GeoJSON.FeatureCollection (jsonObject);
			} else {
				collection = new GeoJSON.FeatureCollection ();
				collection.features.Add (new GeoJSON.FeatureObject (jsonObject));
			}

			return collection;
		}

		virtual public JSONObject Serialize () {

			JSONObject rootObject = new JSONObject (JSONObject.Type.Object);
			rootObject.AddField ("type", type);

			SerializeContent (rootObject);

			return rootObject;
		}

		protected virtual void SerializeContent(JSONObject rootObject) {}
	}
}