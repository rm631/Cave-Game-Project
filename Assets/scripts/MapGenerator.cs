using UnityEngine;
using System.Collections;
using System.Collections.Generic; //Required for the Lists
using System;
using UnityEngine.UI; // UI

public class MapGenerator : MonoBehaviour {
	
	public Text levelText; 
	public GameObject levelImage; 
	public float timeLeft = 30f; 
	
	public static bool canMove = true;
	
	public static int level = 1; 
	
	void Update() {
		timeLeft -= Time.deltaTime;
		if( timeLeft <= 0 ) {
			
			canMove = false;
			
			Invoke("LevelText", 2);
			
			timeLeft = ( 30f * level );
			
			Invoke("GenerateMap", 2);
		}
		/*
		if (Input.GetMouseButtonDown(0)) {
			GenerateMap();
		}
		*/
		if (Input.GetKeyUp(KeyCode.L)) {
			GenerateMap();
		}
	}

	void LevelText() {
		Debug.Log( "Level text should now be shown." );
		levelText.text = "Level " + level;
		levelImage.SetActive(true);
		
		Invoke( "HideLevelText", 5 );
		
	}
	
	void HideLevelText(){
		
		levelImage.SetActive(false);
		
		canMove = true;
		
	}
	
	/*
		To avoid the many issues created when trying to call the GenerateMap function from the MapGenerator script, I made a copy
		of the script within this class, while this may be inefficient, it does solve the immediate issue, allowing for 
		progress and further testing.
		
		// This has now been edited more, by instead moving the LevelController into the MapGenerator script so that MeshGenerator 
		// and other scripts (and other game objects) that are required for building the map are actually present where they should be.
	*/
	
	public int width;
	public int height;
	
	public string seed;
	public bool useRandomSeed;
	
	[Range(0,100)] // limits the range of the variable randomFillPercent between 0 and 100
	public int randomFillPercent;
	
	int[,] map; // Creates a 2D array of values, identifier 'map'
	
	void Start() {  // Called at the start
		GenerateMap(); // Calls the generatemap method
	}
	/*
	
	// As there is an existing void Update() I have to merge them into one and hence have choosen the higher up one, the alternative
	// is to switch this to a fixed update - but this is best for a physics change which this isn't, hence I've choosen to merge.
	
	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			GenerateMap();
		}
	}
	*/ 
	void GenerateMap() { // This method is called at the start and is the method for creating the map
		map = new int[width,height]; //Assigns map the variable of the width and height of the map
		RandomFillMap(); //Calls the RandomFillMap function
		
		for (int i = 0; i < 5; i ++) {
			SmoothMap();
		}
		
		level = level + 1; //Increments the level variable by one
			Debug.Log ( "The Current Level is: " + level ); //Sends a message to console
			
		ProcessMap (); //Calls the ProcessMap function
		
		int borderSize = 1;
		int[,] borderedMap = new int[width + borderSize * 2,height + borderSize * 2]; //Creates and assigns a value to an array or integers
		
		for (int x = 0; x < borderedMap.GetLength(0); x ++) {
			for (int y = 0; y < borderedMap.GetLength(1); y ++) {
				if (x >= borderSize && x < width + borderSize && y >= borderSize && y < height + borderSize) {
					borderedMap[x,y] = map[x-borderSize,y-borderSize];
				}
				else {
					borderedMap[x,y] =1;
				}
			}
		}
		
		MeshGenerator meshGen = GetComponent<MeshGenerator>();
		meshGen.GenerateMesh(borderedMap, 1); // 1 is the defualt squareSize value
	}
	
	void ProcessMap() { // Essentially this will be for removing the small floating blocks
		
		List<List<Coord>> wallRegions = GetRegions (1); // 1 is wall
		// Default value for wallThresholdSize = 50;
		int wallThresholdSize = 50; // The 50 will be used like this - 'any wall made up of less than 50 tiles will be removed'

		foreach (List<Coord> wallRegion in wallRegions) {
			if (wallRegion.Count < wallThresholdSize) {
				foreach (Coord tile in wallRegion) {
					map[tile.tileX,tile.tileY] = 0;
				}
			}
		}
		
		List<List<Coord>> roomRegions = GetRegions (0);
		int roomThresholdSize = 50;
		List<Room> survivingRooms = new List<Room> ();
		
		foreach (List<Coord> roomRegion in roomRegions) {
			if (roomRegion.Count < roomThresholdSize) {
				foreach (Coord tile in roomRegion) {
					
					map[tile.tileX,tile.tileY] = 1;
					
				}
				
			} else {
				
				survivingRooms.Add(new Room(roomRegion, map));
				
			}
			
		}
			
		survivingRooms.Sort ();
		survivingRooms[0].isMainRoom = true;
		survivingRooms[0].isAccessibleFromMainRoom = true;	
		ConnectClosestRooms (survivingRooms);
	}
	
	void ConnectClosestRooms(List<Room> allRooms, bool forceAccessibilityFromMainRoom = false) {
		
		List<Room> roomListA = new List<Room>();
		List<Room> roomListB = new List<Room>();
		
		if( forceAccessibilityFromMainRoom ) {
			foreach( Room room in allRooms ){
				if( room.isAccessibleFromMainRoom ) {
					roomListB.Add (room);
				} else {
					roomListA.Add (room);
				}
				
			}
			
		} else {
			roomListA = allRooms;
			roomListB = allRooms;
		}
		
		int bestDistance = 0;
		
		Coord bestTileA = new Coord ();
		Coord bestTileB = new Coord ();
		
		Room bestRoomA = new Room ();
		Room bestRoomB = new Room ();
		
		bool possibleConnectionFound = false;
		
		foreach (Room roomA in roomListA) {
			if( !forceAccessibilityFromMainRoom ) {
				possibleConnectionFound = false;
				if( roomA.connectedRooms.Count > 0 ){
					continue;
				}
			}
			
			foreach (Room roomB in roomListB) {
				if (roomA == roomB || roomA.IsConnected(roomB)) {
					continue; // Skip to next roomB
				}
			//	if (roomA.IsConnected(roomB)) {
			//		possibleConnectionFound = false;
			//		break; // Skip the next section of code - next roomA
			//	}
				
				for (int tileIndexA = 0; tileIndexA < roomA.edgeTiles.Count; tileIndexA ++) {
					for (int tileIndexB = 0; tileIndexB < roomB.edgeTiles.Count; tileIndexB ++) {
						Coord tileA = roomA.edgeTiles[tileIndexA];
						Coord tileB = roomB.edgeTiles[tileIndexB];
						int distanceBetweenRooms = (int)(Mathf.Pow (tileA.tileX-tileB.tileX,2) + Mathf.Pow (tileA.tileY-tileB.tileY,2));
						// The (int) stores the result of Mathf.Pow is a float (f)
						if (distanceBetweenRooms < bestDistance || !possibleConnectionFound) {
							
							bestDistance = distanceBetweenRooms;
							possibleConnectionFound = true;
							
							bestTileA = tileA;
							bestTileB = tileB;
							bestRoomA = roomA;
							bestRoomB = roomB;
							
						}
						
					}
					
				}
				
			}
			
			if (possibleConnectionFound && !forceAccessibilityFromMainRoom) {
				CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
			}
		}
		
		if (possibleConnectionFound && forceAccessibilityFromMainRoom) {
			CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
			ConnectClosestRooms( allRooms, true );
		}
		
		
		if( !forceAccessibilityFromMainRoom ) {
			
			ConnectClosestRooms( allRooms, true );
			
		}
		
	}
	
	void CreatePassage(Room roomA, Room roomB, Coord tileA, Coord tileB) {
		Room.ConnectRooms (roomA, roomB);
		//Debug.DrawLine (CoordToWorldPoint (tileA), CoordToWorldPoint (tileB), Color.green, 100);-- during testing used to show the shortest connection point between two rooms
		
		List<Coord> line = GetLine (tileA, tileB);
		foreach( Coord c in line ) {
			DrawCircle(c,5); // (c,5) works best
		}
	}
	
	void DrawCircle( Coord c, int r ) {
		for( int x = -r; x <= r; x++ ) {
			for( int y = -r; y <= r; y++ ) {
				if( x*x + y*y <= r*r ) {
					int drawX = c.tileX + x;
					int drawY = c.tileY + y;
					if( IsInMapRange(drawX, drawY) ) {
						map[drawX, drawY] = 0;
					}
				}
			}
		}
	}
	
	List<Coord> GetLine( Coord from, Coord to ) {
		List<Coord> line = new List<Coord>();
		
		int x = from.tileX;
		int y = from.tileY;
		
		int dx = to.tileX - from.tileX;
		int dy = to.tileY - from.tileY;
		
		bool inverted = false;
		int step = Math.Sign( dx );
		int gradientStep = Math.Sign ( dy );
		
		int longest = Mathf.Abs ( dx );
		int shortest = Mathf.Abs ( dy );
		
		if( longest < shortest ) {
			inverted = true;
			longest = Mathf.Abs (dy);
			shortest = Mathf.Abs (dx);
			
			step = Math.Sign (dy);
			gradientStep = Math.Sign (dx);
		}
		
		int gradientAccumulation = longest / 2; //dx/2
		for( int i = 0; i < longest; i++ ) {
			line.Add (new Coord(x,y));
			
			if( inverted ) {
				y += step;
			} else {
				x += step;
			}
			
			gradientAccumulation += shortest;
			if( gradientAccumulation >= longest ) {
				if( inverted ) {
					x += gradientStep;
				} else {
					y += gradientStep;
				}
				gradientStep -= longest;
			}
			
		}
		
		return line;
		
	}
	
	Vector3 CoordToWorldPoint(Coord tile) {
		return new Vector3 (-width / 2 + .5f + tile.tileX, 2, -height / 2 + .5f + tile.tileY);
	}
	
	List<List<Coord>> GetRegions(int tileType) {
		List<List<Coord>> regions = new List<List<Coord>> ();
		int[,] mapFlags = new int[width,height];
		
		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				if (mapFlags[x,y] == 0 && map[x,y] == tileType) {
					List<Coord> newRegion = GetRegionTiles(x,y);
					regions.Add(newRegion);
					
					foreach (Coord tile in newRegion) {
						
						mapFlags[tile.tileX, tile.tileY] = 1;
						
					}
					
				}
				
			}
			
		}
		
		return regions;
	}
	
	List<Coord> GetRegionTiles(int startX, int startY) {
		List<Coord> tiles = new List<Coord> ();
		int[,] mapFlags = new int[width,height];
		int tileType = map [startX, startY];
		
		Queue<Coord> queue = new Queue<Coord> ();
		queue.Enqueue (new Coord (startX, startY));
		mapFlags [startX, startY] = 1;
		
		while (queue.Count > 0) {
			Coord tile = queue.Dequeue();
			tiles.Add(tile);
			
			for (int x = tile.tileX - 1; x <= tile.tileX + 1; x++) {
				for (int y = tile.tileY - 1; y <= tile.tileY + 1; y++) {
					if (IsInMapRange(x,y) && (y == tile.tileY || x == tile.tileX)) {
						if (mapFlags[x,y] == 0 && map[x,y] == tileType) {
							
							mapFlags[x,y] = 1;
							queue.Enqueue(new Coord(x,y));
							
						}
						
					}
					
				}
				
			}
			
		}
		
		return tiles;
	}
	
	bool IsInMapRange(int x, int y) {
		
		return x >= 0 && x < width && y >= 0 && y < height;
		
	}
	
	
	void RandomFillMap() {
		if (useRandomSeed) {
			
			seed = "0";//Time.time.ToString();
			
		}
		
		System.Random pseudoRandom = new System.Random(seed.GetHashCode());
		
		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				if (x == 0 || x == width-1 || y == 0 || y == height -1) {
				
					map[x,y] = 1;
					
				}
				else {
					
					map[x,y] = (pseudoRandom.Next(0,100) < randomFillPercent)? 1: 0;
					
				}
				
			}
			
		}
		
	}
	
	void SmoothMap() {
		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				int neighbourWallTiles = GetSurroundingWallCount(x,y);
				
				if (neighbourWallTiles > 4)
					map[x,y] = 1;
				else if (neighbourWallTiles < 4)
					map[x,y] = 0;
				
			}
			
		}
		
	}
	
	int GetSurroundingWallCount(int gridX, int gridY) {
		int wallCount = 0;
		for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX ++) {
			for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY ++) {
				if (IsInMapRange(neighbourX,neighbourY)) {
					if (neighbourX != gridX || neighbourY != gridY) {
						wallCount += map[neighbourX,neighbourY];
					}
				}
				else {
				
					wallCount ++;
					
				}
				
			}
			
		}
		
		return wallCount;
	}
	
	struct Coord {
		public int tileX;
		public int tileY;
		
		public Coord(int x, int y) {
			
			tileX = x;
			tileY = y;
			
		}
		
	}
	
	
	class Room : IComparable<Room> {
		
		
		//Initalises three lists for the titles, edgeTiles and connectedRooms
		public List<Coord> tiles;
		public List<Coord> edgeTiles;
		public List<Room> connectedRooms;
		
		public int roomSize; //Initalises a public integer called by the identifier roomSize
		
		public bool isAccessibleFromMainRoom;
		public bool isMainRoom;
		
		public Room() {
			
			
			
		}
		
		public Room(List<Coord> roomTiles, int[,] map) {
			tiles = roomTiles;
			roomSize = tiles.Count;
			connectedRooms = new List<Room>();
			
			edgeTiles = new List<Coord>();
			foreach (Coord tile in tiles) {
				for (int x = tile.tileX-1; x <= tile.tileX+1; x++) {
					for (int y = tile.tileY-1; y <= tile.tileY+1; y++) {
						if (x == tile.tileX || y == tile.tileY) {
							if (map[x,y] == 1) {
								
								edgeTiles.Add(tile);
								
							}
							
						}
						
					}
					
				}
				
			}
			
		}
		
		public void SetAccessibleFromMainRoom() {
			
			if( !isAccessibleFromMainRoom ) {
				isAccessibleFromMainRoom = true; 
				foreach( Room connectedRoom in connectedRooms ){
					connectedRoom.SetAccessibleFromMainRoom(); 
				}
			}
			
		}
		
		public static void ConnectRooms(Room roomA, Room roomB) {
			if (roomA.isAccessibleFromMainRoom) {
				roomB.SetAccessibleFromMainRoom ();
			} else if (roomB.isAccessibleFromMainRoom) {
				roomA.SetAccessibleFromMainRoom();
			}
			roomA.connectedRooms.Add (roomB);
			roomB.connectedRooms.Add (roomA);
			
		}
		
		public bool IsConnected(Room otherRoom) {
			
			return connectedRooms.Contains(otherRoom);
			
		}
		
		public int CompareTo( Room otherRoom ) {
			
			return otherRoom.roomSize.CompareTo(roomSize);
			
		}
		
	}

}