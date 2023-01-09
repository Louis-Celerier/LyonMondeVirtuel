using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Defective.JSON;

namespace GeoJSON {
	
	[System.Serializable]
	public class FeatureObject {
	
		public string type;
		public GeometryObject geometry;
		public Dictionary<string, string> properties;

		public FeatureObject(JSONObject jsonObject) {
			type = jsonObject ["type"].stringValue;
			geometry = parseGeometry (jsonObject ["geometry"]);

			properties = new Dictionary<string, string> ();
			parseProperties (jsonObject ["properties"]);
		}
		public FeatureObject(string encodedString) {
			JSONObject jsonObject = new JSONObject (encodedString);
			type = jsonObject ["type"].stringValue;
			geometry = parseGeometry (jsonObject ["geometry"]);

			properties = new Dictionary<string, string> ();
			parseProperties (jsonObject ["properties"]);
		}

		public FeatureObject(GeometryObject featureGeometry) {
			type = "Feature";
			geometry = featureGeometry;

			properties = new Dictionary<string, string> ();
		}

		protected void parseProperties(JSONObject jsonObject) {
			for(int i = 0; i < jsonObject.list.Count; i++){
				string key = (string)jsonObject.keys[i];
				JSONObject value = (JSONObject)jsonObject.list[i];
				if(value.isString)
					properties.Add (key, value.stringValue);
				if(value.isNumber)
					properties.Add (key, value.floatValue.ToString());
			}			
		}

		protected GeometryObject parseGeometry(JSONObject jsonObject){
			switch (jsonObject["type"].stringValue) {
			case "Point":
				return new PointGeometryObject (jsonObject);
			case "MultiPoint":
				return new MultiPointGeometryObject (jsonObject);
			case "LineString":
				return new LineStringGeometryObject (jsonObject);
			case "MultiLineString":
				return new MultiLineStringGeometryObject (jsonObject);
			case "Polygon":
				return new PolygonGeometryObject (jsonObject);
			case "MultiPolygon":
				return new MultiPolygonGeometryObject (jsonObject);
			default:
				break;
			}
			return null;
		}

		public JSONObject Serialize() {
			JSONObject rootObject = new JSONObject(JSONObject.Type.Object);

			rootObject.AddField("type", type);

			//Geometry
			JSONObject geometryObject = geometry.Serialize ();
			rootObject.AddField ("geometry", geometryObject);

			//Properties
			JSONObject jsonProperties = new JSONObject(JSONObject.Type.Object);
			foreach (KeyValuePair<string,string> property in properties) {
				jsonProperties.AddField (property.Key, property.Value);
			}
			rootObject.AddField("properties", jsonProperties);

			return rootObject;
		}
	}
}
