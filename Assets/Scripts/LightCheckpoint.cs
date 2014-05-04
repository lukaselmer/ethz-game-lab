using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
	public class LightCheckpoint {

		public LightCheckpoint (Vector3 position) {
			Position = position;
		}

		private Vector3 _position;
		
		public Vector3 Position {
			get{ return _position;} 
			set{ _position = value;}
		}

	}

}

