﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LookupTables
{
  private static Dictionary<int, List<List<int>>> TriangleList = new Dictionary<int, List<List<int>>>()
  {
  { 0, new List<List<int>>() { } },
  { 1, new List<List<int>>() {new List<int>() {0, 8, 3} } },
  { 2, new List<List<int>>() {new List<int>() {0, 1, 9} } },
  { 3, new List<List<int>>() {new List<int>() {1, 8, 3}, new List<int>() {9, 8, 1} } },
  { 4, new List<List<int>>() {new List<int>() {1, 2, 10} } },
  { 5, new List<List<int>>() {new List<int>() {0, 8, 3}, new List<int>() {1, 2, 10} } },
  { 6, new List<List<int>>() {new List<int>() {9, 2, 10}, new List<int>() {0, 2, 9} } },
  { 7, new List<List<int>>() {new List<int>() {2, 8, 3}, new List<int>() {2, 10, 8}, new List<int>() {10, 9, 8} } },
  { 8, new List<List<int>>() {new List<int>() {3, 11, 2} } },
  { 9, new List<List<int>>() {new List<int>() {0, 11, 2}, new List<int>() {8, 11, 0} } },
  { 10, new List<List<int>>() {new List<int>() {1, 9, 0}, new List<int>() {2, 3, 11} } },
  { 11, new List<List<int>>() {new List<int>() {1, 11, 2}, new List<int>() {1, 9, 11}, new List<int>() {9, 8, 11} } },
  { 12, new List<List<int>>() {new List<int>() {3, 10, 1}, new List<int>() {11, 10, 3} } },
  { 13, new List<List<int>>() {new List<int>() {0, 10, 1}, new List<int>() {0, 8, 10}, new List<int>() {8, 11, 10} } },
  { 14, new List<List<int>>() {new List<int>() {3, 9, 0}, new List<int>() {3, 11, 9}, new List<int>() {11, 10, 9} } },
  { 15, new List<List<int>>() {new List<int>() {9, 8, 10}, new List<int>() {10, 8, 11} } },
  { 16, new List<List<int>>() {new List<int>() {4, 7, 8} } },
  { 17, new List<List<int>>() {new List<int>() {4, 3, 0}, new List<int>() {7, 3, 4} } },
  { 18, new List<List<int>>() {new List<int>() {0, 1, 9}, new List<int>() {8, 4, 7} } },
  { 19, new List<List<int>>() {new List<int>() {4, 1, 9}, new List<int>() {4, 7, 1}, new List<int>() {7, 3, 1} } },
  { 20, new List<List<int>>() {new List<int>() {1, 2, 10}, new List<int>() {8, 4, 7} } },
  { 21, new List<List<int>>() {new List<int>() {3, 4, 7}, new List<int>() {3, 0, 4}, new List<int>() {1, 2, 10} } },
  { 22, new List<List<int>>() {new List<int>() {9, 2, 10}, new List<int>() {9, 0, 2}, new List<int>() {8, 4, 7} } },
  { 23, new List<List<int>>() {new List<int>() {2, 10, 9}, new List<int>() {2, 9, 7}, new List<int>() {2, 7, 3}, new List<int>() {7, 9, 4} } },
  { 24, new List<List<int>>() {new List<int>() {8, 4, 7}, new List<int>() {3, 11, 2} } },
  { 25, new List<List<int>>() {new List<int>() {11, 4, 7}, new List<int>() {11, 2, 4}, new List<int>() {2, 0, 4} } },
  { 26, new List<List<int>>() {new List<int>() {9, 0, 1}, new List<int>() {8, 4, 7}, new List<int>() {2, 3, 11} } },
  { 27, new List<List<int>>() {new List<int>() {4, 7, 11}, new List<int>() {9, 4, 11}, new List<int>() {9, 11, 2}, new List<int>() {9, 2, 1} } },
  { 28, new List<List<int>>() {new List<int>() {3, 10, 1}, new List<int>() {3, 11, 10}, new List<int>() {7, 8, 4} } },
  { 29, new List<List<int>>() {new List<int>() {1, 11, 10}, new List<int>() {1, 4, 11}, new List<int>() {1, 0, 4}, new List<int>() {7, 11, 4} } },
  { 30, new List<List<int>>() {new List<int>() {4, 7, 8}, new List<int>() {9, 0, 11}, new List<int>() {9, 11, 10}, new List<int>() {11, 0, 3} } },
  { 31, new List<List<int>>() {new List<int>() {4, 7, 11}, new List<int>() {4, 11, 9}, new List<int>() {9, 11, 10} } },
  { 32, new List<List<int>>() {new List<int>() {9, 5, 4} } },
  { 33, new List<List<int>>() {new List<int>() {9, 5, 4}, new List<int>() {0, 8, 3} } },
  { 34, new List<List<int>>() {new List<int>() {0, 5, 4}, new List<int>() {1, 5, 0} } },
  { 35, new List<List<int>>() {new List<int>() {8, 5, 4}, new List<int>() {8, 3, 5}, new List<int>() {3, 1, 5} } },
  { 36, new List<List<int>>() {new List<int>() {1, 2, 10}, new List<int>() {9, 5, 4} } },
  { 37, new List<List<int>>() {new List<int>() {3, 0, 8}, new List<int>() {1, 2, 10}, new List<int>() {4, 9, 5} } },
  { 38, new List<List<int>>() {new List<int>() {5, 2, 10}, new List<int>() {5, 4, 2}, new List<int>() {4, 0, 2} } },
  { 39, new List<List<int>>() {new List<int>() {2, 10, 5}, new List<int>() {3, 2, 5}, new List<int>() {3, 5, 4}, new List<int>() {3, 4, 8} } },
  { 40, new List<List<int>>() {new List<int>() {9, 5, 4}, new List<int>() {2, 3, 11} } },
  { 41, new List<List<int>>() {new List<int>() {0, 11, 2}, new List<int>() {0, 8, 11}, new List<int>() {4, 9, 5} } },
  { 42, new List<List<int>>() {new List<int>() {0, 5, 4}, new List<int>() {0, 1, 5}, new List<int>() {2, 3, 11} } },
  { 43, new List<List<int>>() {new List<int>() {2, 1, 5}, new List<int>() {2, 5, 8}, new List<int>() {2, 8, 11}, new List<int>() {4, 8, 5} } },
  { 44, new List<List<int>>() {new List<int>() {10, 3, 11}, new List<int>() {10, 1, 3}, new List<int>() {9, 5, 4} } },
  { 45, new List<List<int>>() {new List<int>() {4, 9, 5}, new List<int>() {0, 8, 1}, new List<int>() {8, 10, 1}, new List<int>() {8, 11, 10} } },
  { 46, new List<List<int>>() {new List<int>() {5, 4, 0}, new List<int>() {5, 0, 11}, new List<int>() {5, 11, 10}, new List<int>() {11, 0, 3} } },
  { 47, new List<List<int>>() {new List<int>() {5, 4, 8}, new List<int>() {5, 8, 10}, new List<int>() {10, 8, 11} } },
  { 48, new List<List<int>>() {new List<int>() {9, 7, 8}, new List<int>() {5, 7, 9} } },
  { 49, new List<List<int>>() {new List<int>() {9, 3, 0}, new List<int>() {9, 5, 3}, new List<int>() {5, 7, 3} } },
  { 50, new List<List<int>>() {new List<int>() {0, 7, 8}, new List<int>() {0, 1, 7}, new List<int>() {1, 5, 7} } },
  { 51, new List<List<int>>() {new List<int>() {1, 5, 3}, new List<int>() {3, 5, 7} } },
  { 52, new List<List<int>>() {new List<int>() {9, 7, 8}, new List<int>() {9, 5, 7}, new List<int>() {10, 1, 2} } },
  { 53, new List<List<int>>() {new List<int>() {10, 1, 2}, new List<int>() {9, 5, 0}, new List<int>() {5, 3, 0}, new List<int>() {5, 7, 3} } },
  { 54, new List<List<int>>() {new List<int>() {8, 0, 2}, new List<int>() {8, 2, 5}, new List<int>() {8, 5, 7}, new List<int>() {10, 5, 2} } },
  { 55, new List<List<int>>() {new List<int>() {2, 10, 5}, new List<int>() {2, 5, 3}, new List<int>() {3, 5, 7} } },
  { 56, new List<List<int>>() {new List<int>() {7, 9, 5}, new List<int>() {7, 8, 9}, new List<int>() {3, 11, 2} } },
  { 57, new List<List<int>>() {new List<int>() {9, 5, 7}, new List<int>() {9, 7, 2}, new List<int>() {9, 2, 0}, new List<int>() {2, 7, 11} } },
  { 58, new List<List<int>>() {new List<int>() {2, 3, 11}, new List<int>() {0, 1, 8}, new List<int>() {1, 7, 8}, new List<int>() {1, 5, 7} } },
  { 59, new List<List<int>>() {new List<int>() {11, 2, 1}, new List<int>() {11, 1, 7}, new List<int>() {7, 1, 5} } },
  { 60, new List<List<int>>() {new List<int>() {9, 5, 8}, new List<int>() {8, 5, 7}, new List<int>() {10, 1, 3}, new List<int>() {10, 3, 11} } },
  { 61, new List<List<int>>() {new List<int>() {5, 7, 0}, new List<int>() {5, 0, 9}, new List<int>() {7, 11, 0}, new List<int>() {1, 0, 10}, new List<int>() {11, 10, 0}, } },
  { 62, new List<List<int>>() {new List<int>() {11, 10, 0}, new List<int>() {11, 0, 3}, new List<int>() {10, 5, 0}, new List<int>() {8, 0, 7}, new List<int>() {5, 7, 0}, } },
  { 63, new List<List<int>>() {new List<int>() {11, 10, 5}, new List<int>() {7, 11, 5} } },
  { 64, new List<List<int>>() {new List<int>() {10, 6, 5} } },
  { 65, new List<List<int>>() {new List<int>() {0, 8, 3}, new List<int>() {5, 10, 6} } },
  { 66, new List<List<int>>() {new List<int>() {9, 0, 1}, new List<int>() {5, 10, 6} } },
  { 67, new List<List<int>>() {new List<int>() {1, 8, 3}, new List<int>() {1, 9, 8}, new List<int>() {5, 10, 6} } },
  { 68, new List<List<int>>() {new List<int>() {1, 6, 5}, new List<int>() {2, 6, 1} } },
  { 69, new List<List<int>>() {new List<int>() {1, 6, 5}, new List<int>() {1, 2, 6}, new List<int>() {3, 0, 8} } },
  { 70, new List<List<int>>() {new List<int>() {9, 6, 5}, new List<int>() {9, 0, 6}, new List<int>() {0, 2, 6} } },
  { 71, new List<List<int>>() {new List<int>() {5, 9, 8}, new List<int>() {5, 8, 2}, new List<int>() {5, 2, 6}, new List<int>() {3, 2, 8} } },
  { 72, new List<List<int>>() {new List<int>() {2, 3, 11}, new List<int>() {10, 6, 5} } },
  { 73, new List<List<int>>() {new List<int>() {11, 0, 8}, new List<int>() {11, 2, 0}, new List<int>() {10, 6, 5} } },
  { 74, new List<List<int>>() {new List<int>() {0, 1, 9}, new List<int>() {2, 3, 11}, new List<int>() {5, 10, 6} } },
  { 75, new List<List<int>>() {new List<int>() {5, 10, 6}, new List<int>() {1, 9, 2}, new List<int>() {9, 11, 2}, new List<int>() {9, 8, 11} } },
  { 76, new List<List<int>>() {new List<int>() {6, 3, 11}, new List<int>() {6, 5, 3}, new List<int>() {5, 1, 3} } },
  { 77, new List<List<int>>() {new List<int>() {0, 8, 11}, new List<int>() {0, 11, 5}, new List<int>() {0, 5, 1}, new List<int>() {5, 11, 6} } },
  { 78, new List<List<int>>() {new List<int>() {3, 11, 6}, new List<int>() {0, 3, 6}, new List<int>() {0, 6, 5}, new List<int>() {0, 5, 9} } },
  { 79, new List<List<int>>() {new List<int>() {6, 5, 9}, new List<int>() {6, 9, 11}, new List<int>() {11, 9, 8} } },
  { 80, new List<List<int>>() {new List<int>() {5, 10, 6}, new List<int>() {4, 7, 8} } },
  { 81, new List<List<int>>() {new List<int>() {4, 3, 0}, new List<int>() {4, 7, 3}, new List<int>() {6, 5, 10} } },
  { 82, new List<List<int>>() {new List<int>() {1, 9, 0}, new List<int>() {5, 10, 6}, new List<int>() {8, 4, 7} } },
  { 83, new List<List<int>>() {new List<int>() {10, 6, 5}, new List<int>() {1, 9, 7}, new List<int>() {1, 7, 3}, new List<int>() {7, 9, 4} } },
  { 84, new List<List<int>>() {new List<int>() {6, 1, 2}, new List<int>() {6, 5, 1}, new List<int>() {4, 7, 8} } },
  { 85, new List<List<int>>() {new List<int>() {1, 2, 5}, new List<int>() {5, 2, 6}, new List<int>() {3, 0, 4}, new List<int>() {3, 4, 7} } },
  { 86, new List<List<int>>() {new List<int>() {8, 4, 7}, new List<int>() {9, 0, 5}, new List<int>() {0, 6, 5}, new List<int>() {0, 2, 6} } },
  { 87, new List<List<int>>() {new List<int>() {7, 3, 9}, new List<int>() {7, 9, 4}, new List<int>() {3, 2, 9}, new List<int>() {5, 9, 6}, new List<int>() {2, 6, 9}, } },
  { 88, new List<List<int>>() {new List<int>() {3, 11, 2}, new List<int>() {7, 8, 4}, new List<int>() {10, 6, 5} } },
  { 89, new List<List<int>>() {new List<int>() {5, 10, 6}, new List<int>() {4, 7, 2}, new List<int>() {4, 2, 0}, new List<int>() {2, 7, 11} } },
  { 90, new List<List<int>>() {new List<int>() {0, 1, 9}, new List<int>() {4, 7, 8}, new List<int>() {2, 3, 11}, new List<int>() {5, 10, 6} } },
  { 91, new List<List<int>>() {new List<int>() {9, 2, 1}, new List<int>() {9, 11, 2}, new List<int>() {9, 4, 11}, new List<int>() {7, 11, 4}, new List<int>() {5, 10, 6}, } },
  { 92, new List<List<int>>() {new List<int>() {8, 4, 7}, new List<int>() {3, 11, 5}, new List<int>() {3, 5, 1}, new List<int>() {5, 11, 6} } },
  { 93, new List<List<int>>() {new List<int>() {5, 1, 11}, new List<int>() {5, 11, 6}, new List<int>() {1, 0, 11}, new List<int>() {7, 11, 4}, new List<int>() {0, 4, 11}, } },
  { 94, new List<List<int>>() {new List<int>() {0, 5, 9}, new List<int>() {0, 6, 5}, new List<int>() {0, 3, 6}, new List<int>() {11, 6, 3}, new List<int>() {8, 4, 7}, } },
  { 95, new List<List<int>>() {new List<int>() {6, 5, 9}, new List<int>() {6, 9, 11}, new List<int>() {4, 7, 9}, new List<int>() {7, 11, 9} } },
  { 96, new List<List<int>>() {new List<int>() {10, 4, 9}, new List<int>() {6, 4, 10} } },
  { 97, new List<List<int>>() {new List<int>() {4, 10, 6}, new List<int>() {4, 9, 10}, new List<int>() {0, 8, 3} } },
  { 98, new List<List<int>>() {new List<int>() {10, 0, 1}, new List<int>() {10, 6, 0}, new List<int>() {6, 4, 0} } },
  { 99, new List<List<int>>() {new List<int>() {8, 3, 1}, new List<int>() {8, 1, 6}, new List<int>() {8, 6, 4}, new List<int>() {6, 1, 10} } },
  { 100, new List<List<int>>() {new List<int>() {1, 4, 9}, new List<int>() {1, 2, 4}, new List<int>() {2, 6, 4} } },
  { 101, new List<List<int>>() {new List<int>() {3, 0, 8}, new List<int>() {1, 2, 9}, new List<int>() {2, 4, 9}, new List<int>() {2, 6, 4} } },
  { 102, new List<List<int>>() {new List<int>() {0, 2, 4}, new List<int>() {4, 2, 6} } },
  { 103, new List<List<int>>() {new List<int>() {8, 3, 2}, new List<int>() {8, 2, 4}, new List<int>() {4, 2, 6} } },
  { 104, new List<List<int>>() {new List<int>() {10, 4, 9}, new List<int>() {10, 6, 4}, new List<int>() {11, 2, 3} } },
  { 105, new List<List<int>>() {new List<int>() {0, 8, 2}, new List<int>() {2, 8, 11}, new List<int>() {4, 9, 10}, new List<int>() {4, 10, 6} } },
  { 106, new List<List<int>>() {new List<int>() {3, 11, 2}, new List<int>() {0, 1, 6}, new List<int>() {0, 6, 4}, new List<int>() {6, 1, 10} } },
  { 107, new List<List<int>>() {new List<int>() {6, 4, 1}, new List<int>() {6, 1, 10}, new List<int>() {4, 8, 1}, new List<int>() {2, 1, 11}, new List<int>() {8, 11, 1}, } },
  { 108, new List<List<int>>() {new List<int>() {9, 6, 4}, new List<int>() {9, 3, 6}, new List<int>() {9, 1, 3}, new List<int>() {11, 6, 3} } },
  { 109, new List<List<int>>() {new List<int>() {8, 11, 1}, new List<int>() {8, 1, 0}, new List<int>() {11, 6, 1}, new List<int>() {9, 1, 4}, new List<int>() {6, 4, 1}, } },
  { 110, new List<List<int>>() {new List<int>() {3, 11, 6}, new List<int>() {3, 6, 0}, new List<int>() {0, 6, 4} } },
  { 111, new List<List<int>>() {new List<int>() {6, 4, 8}, new List<int>() {11, 6, 8} } },
  { 112, new List<List<int>>() {new List<int>() {7, 10, 6}, new List<int>() {7, 8, 10}, new List<int>() {8, 9, 10} } },
  { 113, new List<List<int>>() {new List<int>() {0, 7, 3}, new List<int>() {0, 10, 7}, new List<int>() {0, 9, 10}, new List<int>() {6, 7, 10} } },
  { 114, new List<List<int>>() {new List<int>() {10, 6, 7}, new List<int>() {1, 10, 7}, new List<int>() {1, 7, 8}, new List<int>() {1, 8, 0} } },
  { 115, new List<List<int>>() {new List<int>() {10, 6, 7}, new List<int>() {10, 7, 1}, new List<int>() {1, 7, 3} } },
  { 116, new List<List<int>>() {new List<int>() {1, 2, 6}, new List<int>() {1, 6, 8}, new List<int>() {1, 8, 9}, new List<int>() {8, 6, 7} } },
  { 117, new List<List<int>>() {new List<int>() {2, 6, 9}, new List<int>() {2, 9, 1}, new List<int>() {6, 7, 9}, new List<int>() {0, 9, 3}, new List<int>() {7, 3, 9}, } },
  { 118, new List<List<int>>() {new List<int>() {7, 8, 0}, new List<int>() {7, 0, 6}, new List<int>() {6, 0, 2} } },
  { 119, new List<List<int>>() {new List<int>() {7, 3, 2}, new List<int>() {6, 7, 2} } },
  { 120, new List<List<int>>() {new List<int>() {2, 3, 11}, new List<int>() {10, 6, 8}, new List<int>() {10, 8, 9}, new List<int>() {8, 6, 7} } },
  { 121, new List<List<int>>() {new List<int>() {2, 0, 7}, new List<int>() {2, 7, 11}, new List<int>() {0, 9, 7}, new List<int>() {6, 7, 10}, new List<int>() {9, 10, 7}, } },
  { 122, new List<List<int>>() {new List<int>() {1, 8, 0}, new List<int>() {1, 7, 8}, new List<int>() {1, 10, 7}, new List<int>() {6, 7, 10}, new List<int>() {2, 3, 11}, } },
  { 123, new List<List<int>>() {new List<int>() {11, 2, 1}, new List<int>() {11, 1, 7}, new List<int>() {10, 6, 1}, new List<int>() {6, 7, 1} } },
  { 124, new List<List<int>>() {new List<int>() {8, 9, 6}, new List<int>() {8, 6, 7}, new List<int>() {9, 1, 6}, new List<int>() {11, 6, 3}, new List<int>() {1, 3, 6}, } },
  { 125, new List<List<int>>() {new List<int>() {0, 9, 1}, new List<int>() {11, 6, 7} } },
  { 126, new List<List<int>>() {new List<int>() {7, 8, 0}, new List<int>() {7, 0, 6}, new List<int>() {3, 11, 0}, new List<int>() {11, 6, 0} } },
  { 127, new List<List<int>>() {new List<int>() {7, 11, 6} } },
  { 128, new List<List<int>>() {new List<int>() {7, 6, 11} } },
  { 129, new List<List<int>>() {new List<int>() {3, 0, 8}, new List<int>() {11, 7, 6} } },
  { 130, new List<List<int>>() {new List<int>() {0, 1, 9}, new List<int>() {11, 7, 6} } },
  { 131, new List<List<int>>() {new List<int>() {8, 1, 9}, new List<int>() {8, 3, 1}, new List<int>() {11, 7, 6} } },
  { 132, new List<List<int>>() {new List<int>() {10, 1, 2}, new List<int>() {6, 11, 7} } },
  { 133, new List<List<int>>() {new List<int>() {1, 2, 10}, new List<int>() {3, 0, 8}, new List<int>() {6, 11, 7} } },
  { 134, new List<List<int>>() {new List<int>() {2, 9, 0}, new List<int>() {2, 10, 9}, new List<int>() {6, 11, 7} } },
  { 135, new List<List<int>>() {new List<int>() {6, 11, 7}, new List<int>() {2, 10, 3}, new List<int>() {10, 8, 3}, new List<int>() {10, 9, 8} } },
  { 136, new List<List<int>>() {new List<int>() {7, 2, 3}, new List<int>() {6, 2, 7} } },
  { 137, new List<List<int>>() {new List<int>() {7, 0, 8}, new List<int>() {7, 6, 0}, new List<int>() {6, 2, 0} } },
  { 138, new List<List<int>>() {new List<int>() {2, 7, 6}, new List<int>() {2, 3, 7}, new List<int>() {0, 1, 9} } },
  { 139, new List<List<int>>() {new List<int>() {1, 6, 2}, new List<int>() {1, 8, 6}, new List<int>() {1, 9, 8}, new List<int>() {8, 7, 6} } },
  { 140, new List<List<int>>() {new List<int>() {10, 7, 6}, new List<int>() {10, 1, 7}, new List<int>() {1, 3, 7} } },
  { 141, new List<List<int>>() {new List<int>() {10, 7, 6}, new List<int>() {1, 7, 10}, new List<int>() {1, 8, 7}, new List<int>() {1, 0, 8} } },
  { 142, new List<List<int>>() {new List<int>() {0, 3, 7}, new List<int>() {0, 7, 10}, new List<int>() {0, 10, 9}, new List<int>() {6, 10, 7} } },
  { 143, new List<List<int>>() {new List<int>() {7, 6, 10}, new List<int>() {7, 10, 8}, new List<int>() {8, 10, 9} } },
  { 144, new List<List<int>>() {new List<int>() {6, 8, 4}, new List<int>() {11, 8, 6} } },
  { 145, new List<List<int>>() {new List<int>() {3, 6, 11}, new List<int>() {3, 0, 6}, new List<int>() {0, 4, 6} } },
  { 146, new List<List<int>>() {new List<int>() {8, 6, 11}, new List<int>() {8, 4, 6}, new List<int>() {9, 0, 1} } },
  { 147, new List<List<int>>() {new List<int>() {9, 4, 6}, new List<int>() {9, 6, 3}, new List<int>() {9, 3, 1}, new List<int>() {11, 3, 6} } },
  { 148, new List<List<int>>() {new List<int>() {6, 8, 4}, new List<int>() {6, 11, 8}, new List<int>() {2, 10, 1} } },
  { 149, new List<List<int>>() {new List<int>() {1, 2, 10}, new List<int>() {3, 0, 11}, new List<int>() {0, 6, 11}, new List<int>() {0, 4, 6} } },
  { 150, new List<List<int>>() {new List<int>() {4, 11, 8}, new List<int>() {4, 6, 11}, new List<int>() {0, 2, 9}, new List<int>() {2, 10, 9} } },
  { 151, new List<List<int>>() {new List<int>() {10, 9, 3}, new List<int>() {10, 3, 2}, new List<int>() {9, 4, 3}, new List<int>() {11, 3, 6}, new List<int>() {4, 6, 3}, } },
  { 152, new List<List<int>>() {new List<int>() {8, 2, 3}, new List<int>() {8, 4, 2}, new List<int>() {4, 6, 2} } },
  { 153, new List<List<int>>() {new List<int>() {0, 4, 2}, new List<int>() {4, 6, 2} } },
  { 154, new List<List<int>>() {new List<int>() {1, 9, 0}, new List<int>() {2, 3, 4}, new List<int>() {2, 4, 6}, new List<int>() {4, 3, 8} } },
  { 155, new List<List<int>>() {new List<int>() {1, 9, 4}, new List<int>() {1, 4, 2}, new List<int>() {2, 4, 6} } },
  { 156, new List<List<int>>() {new List<int>() {8, 1, 3}, new List<int>() {8, 6, 1}, new List<int>() {8, 4, 6}, new List<int>() {6, 10, 1} } },
  { 157, new List<List<int>>() {new List<int>() {10, 1, 0}, new List<int>() {10, 0, 6}, new List<int>() {6, 0, 4} } },
  { 158, new List<List<int>>() {new List<int>() {4, 6, 3}, new List<int>() {4, 3, 8}, new List<int>() {6, 10, 3}, new List<int>() {0, 3, 9}, new List<int>() {10, 9, 3}, } },
  { 159, new List<List<int>>() {new List<int>() {10, 9, 4}, new List<int>() {6, 10, 4} } },
  { 160, new List<List<int>>() {new List<int>() {4, 9, 5}, new List<int>() {7, 6, 11} } },
  { 161, new List<List<int>>() {new List<int>() {0, 8, 3}, new List<int>() {4, 9, 5}, new List<int>() {11, 7, 6} } },
  { 162, new List<List<int>>() {new List<int>() {5, 0, 1}, new List<int>() {5, 4, 0}, new List<int>() {7, 6, 11} } },
  { 163, new List<List<int>>() {new List<int>() {11, 7, 6}, new List<int>() {8, 3, 4}, new List<int>() {3, 5, 4}, new List<int>() {3, 1, 5} } },
  { 164, new List<List<int>>() {new List<int>() {9, 5, 4}, new List<int>() {10, 1, 2}, new List<int>() {7, 6, 11} } },
  { 165, new List<List<int>>() {new List<int>() {6, 11, 7}, new List<int>() {1, 2, 10}, new List<int>() {0, 8, 3}, new List<int>() {4, 9, 5} } },
  { 166, new List<List<int>>() {new List<int>() {7, 6, 11}, new List<int>() {5, 4, 10}, new List<int>() {4, 2, 10}, new List<int>() {4, 0, 2} } },
  { 167, new List<List<int>>() {new List<int>() {3, 4, 8}, new List<int>() {3, 5, 4}, new List<int>() {3, 2, 5}, new List<int>() {10, 5, 2}, new List<int>() {11, 7, 6}, } },
  { 168, new List<List<int>>() {new List<int>() {7, 2, 3}, new List<int>() {7, 6, 2}, new List<int>() {5, 4, 9} } },
  { 169, new List<List<int>>() {new List<int>() {9, 5, 4}, new List<int>() {0, 8, 6}, new List<int>() {0, 6, 2}, new List<int>() {6, 8, 7} } },
  { 170, new List<List<int>>() {new List<int>() {3, 6, 2}, new List<int>() {3, 7, 6}, new List<int>() {1, 5, 0}, new List<int>() {5, 4, 0} } },
  { 171, new List<List<int>>() {new List<int>() {6, 2, 8}, new List<int>() {6, 8, 7}, new List<int>() {2, 1, 8}, new List<int>() {4, 8, 5}, new List<int>() {1, 5, 8}, } },
  { 172, new List<List<int>>() {new List<int>() {9, 5, 4}, new List<int>() {10, 1, 6}, new List<int>() {1, 7, 6}, new List<int>() {1, 3, 7} } },
  { 173, new List<List<int>>() {new List<int>() {1, 6, 10}, new List<int>() {1, 7, 6}, new List<int>() {1, 0, 7}, new List<int>() {8, 7, 0}, new List<int>() {9, 5, 4}, } },
  { 174, new List<List<int>>() {new List<int>() {4, 0, 10}, new List<int>() {4, 10, 5}, new List<int>() {0, 3, 10}, new List<int>() {6, 10, 7}, new List<int>() {3, 7, 10}, } },
  { 175, new List<List<int>>() {new List<int>() {7, 6, 10}, new List<int>() {7, 10, 8}, new List<int>() {5, 4, 10}, new List<int>() {4, 8, 10} } },
  { 176, new List<List<int>>() {new List<int>() {6, 9, 5}, new List<int>() {6, 11, 9}, new List<int>() {11, 8, 9} } },
  { 177, new List<List<int>>() {new List<int>() {3, 6, 11}, new List<int>() {0, 6, 3}, new List<int>() {0, 5, 6}, new List<int>() {0, 9, 5} } },
  { 178, new List<List<int>>() {new List<int>() {0, 11, 8}, new List<int>() {0, 5, 11}, new List<int>() {0, 1, 5}, new List<int>() {5, 6, 11} } },
  { 179, new List<List<int>>() {new List<int>() {6, 11, 3}, new List<int>() {6, 3, 5}, new List<int>() {5, 3, 1} } },
  { 180, new List<List<int>>() {new List<int>() {1, 2, 10}, new List<int>() {9, 5, 11}, new List<int>() {9, 11, 8}, new List<int>() {11, 5, 6} } },
  { 181, new List<List<int>>() {new List<int>() {0, 11, 3}, new List<int>() {0, 6, 11}, new List<int>() {0, 9, 6}, new List<int>() {5, 6, 9}, new List<int>() {1, 2, 10}, } },
  { 182, new List<List<int>>() {new List<int>() {11, 8, 5}, new List<int>() {11, 5, 6}, new List<int>() {8, 0, 5}, new List<int>() {10, 5, 2}, new List<int>() {0, 2, 5}, } },
  { 183, new List<List<int>>() {new List<int>() {6, 11, 3}, new List<int>() {6, 3, 5}, new List<int>() {2, 10, 3}, new List<int>() {10, 5, 3} } },
  { 184, new List<List<int>>() {new List<int>() {5, 8, 9}, new List<int>() {5, 2, 8}, new List<int>() {5, 6, 2}, new List<int>() {3, 8, 2} } },
  { 185, new List<List<int>>() {new List<int>() {9, 5, 6}, new List<int>() {9, 6, 0}, new List<int>() {0, 6, 2} } },
  { 186, new List<List<int>>() {new List<int>() {1, 5, 8}, new List<int>() {1, 8, 0}, new List<int>() {5, 6, 8}, new List<int>() {3, 8, 2}, new List<int>() {6, 2, 8}, } },
  { 187, new List<List<int>>() {new List<int>() {1, 5, 6}, new List<int>() {2, 1, 6} } },
  { 188, new List<List<int>>() {new List<int>() {1, 3, 6}, new List<int>() {1, 6, 10}, new List<int>() {3, 8, 6}, new List<int>() {5, 6, 9}, new List<int>() {8, 9, 6}, } },
  { 189, new List<List<int>>() {new List<int>() {10, 1, 0}, new List<int>() {10, 0, 6}, new List<int>() {9, 5, 0}, new List<int>() {5, 6, 0} } },
  { 190, new List<List<int>>() {new List<int>() {0, 3, 8}, new List<int>() {5, 6, 10} } },
  { 191, new List<List<int>>() {new List<int>() {10, 5, 6} } },
  { 192, new List<List<int>>() {new List<int>() {11, 5, 10}, new List<int>() {7, 5, 11} } },
  { 193, new List<List<int>>() {new List<int>() {11, 5, 10}, new List<int>() {11, 7, 5}, new List<int>() {8, 3, 0} } },
  { 194, new List<List<int>>() {new List<int>() {5, 11, 7}, new List<int>() {5, 10, 11}, new List<int>() {1, 9, 0} } },
  { 195, new List<List<int>>() {new List<int>() {10, 7, 5}, new List<int>() {10, 11, 7}, new List<int>() {9, 8, 1}, new List<int>() {8, 3, 1} } },
  { 196, new List<List<int>>() {new List<int>() {11, 1, 2}, new List<int>() {11, 7, 1}, new List<int>() {7, 5, 1} } },
  { 197, new List<List<int>>() {new List<int>() {0, 8, 3}, new List<int>() {1, 2, 7}, new List<int>() {1, 7, 5}, new List<int>() {7, 2, 11} } },
  { 198, new List<List<int>>() {new List<int>() {9, 7, 5}, new List<int>() {9, 2, 7}, new List<int>() {9, 0, 2}, new List<int>() {2, 11, 7} } },
  { 199, new List<List<int>>() {new List<int>() {7, 5, 2}, new List<int>() {7, 2, 11}, new List<int>() {5, 9, 2}, new List<int>() {3, 2, 8}, new List<int>() {9, 8, 2}, } },
  { 200, new List<List<int>>() {new List<int>() {2, 5, 10}, new List<int>() {2, 3, 5}, new List<int>() {3, 7, 5} } },
  { 201, new List<List<int>>() {new List<int>() {8, 2, 0}, new List<int>() {8, 5, 2}, new List<int>() {8, 7, 5}, new List<int>() {10, 2, 5} } },
  { 202, new List<List<int>>() {new List<int>() {9, 0, 1}, new List<int>() {5, 10, 3}, new List<int>() {5, 3, 7}, new List<int>() {3, 10, 2} } },
  { 203, new List<List<int>>() {new List<int>() {9, 8, 2}, new List<int>() {9, 2, 1}, new List<int>() {8, 7, 2}, new List<int>() {10, 2, 5}, new List<int>() {7, 5, 2}, } },
  { 204, new List<List<int>>() {new List<int>() {1, 3, 5}, new List<int>() {3, 7, 5} } },
  { 205, new List<List<int>>() {new List<int>() {0, 8, 7}, new List<int>() {0, 7, 1}, new List<int>() {1, 7, 5} } },
  { 206, new List<List<int>>() {new List<int>() {9, 0, 3}, new List<int>() {9, 3, 5}, new List<int>() {5, 3, 7} } },
  { 207, new List<List<int>>() {new List<int>() {9, 8, 7}, new List<int>() {5, 9, 7} } },
  { 208, new List<List<int>>() {new List<int>() {5, 8, 4}, new List<int>() {5, 10, 8}, new List<int>() {10, 11, 8} } },
  { 209, new List<List<int>>() {new List<int>() {5, 0, 4}, new List<int>() {5, 11, 0}, new List<int>() {5, 10, 11}, new List<int>() {11, 3, 0} } },
  { 210, new List<List<int>>() {new List<int>() {0, 1, 9}, new List<int>() {8, 4, 10}, new List<int>() {8, 10, 11}, new List<int>() {10, 4, 5} } },
  { 211, new List<List<int>>() {new List<int>() {10, 11, 4}, new List<int>() {10, 4, 5}, new List<int>() {11, 3, 4}, new List<int>() {9, 4, 1}, new List<int>() {3, 1, 4}, } },
  { 212, new List<List<int>>() {new List<int>() {2, 5, 1}, new List<int>() {2, 8, 5}, new List<int>() {2, 11, 8}, new List<int>() {4, 5, 8} } },
  { 213, new List<List<int>>() {new List<int>() {0, 4, 11}, new List<int>() {0, 11, 3}, new List<int>() {4, 5, 11}, new List<int>() {2, 11, 1}, new List<int>() {5, 1, 11}, } },
  { 214, new List<List<int>>() {new List<int>() {0, 2, 5}, new List<int>() {0, 5, 9}, new List<int>() {2, 11, 5}, new List<int>() {4, 5, 8}, new List<int>() {11, 8, 5}, } },
  { 215, new List<List<int>>() {new List<int>() {9, 4, 5}, new List<int>() {2, 11, 3} } },
  { 216, new List<List<int>>() {new List<int>() {2, 5, 10}, new List<int>() {3, 5, 2}, new List<int>() {3, 4, 5}, new List<int>() {3, 8, 4} } },
  { 217, new List<List<int>>() {new List<int>() {5, 10, 2}, new List<int>() {5, 2, 4}, new List<int>() {4, 2, 0} } },
  { 218, new List<List<int>>() {new List<int>() {3, 10, 2}, new List<int>() {3, 5, 10}, new List<int>() {3, 8, 5}, new List<int>() {4, 5, 8}, new List<int>() {0, 1, 9}, } },
  { 219, new List<List<int>>() {new List<int>() {5, 10, 2}, new List<int>() {5, 2, 4}, new List<int>() {1, 9, 2}, new List<int>() {9, 4, 2} } },
  { 220, new List<List<int>>() {new List<int>() {8, 4, 5}, new List<int>() {8, 5, 3}, new List<int>() {3, 5, 1} } },
  { 221, new List<List<int>>() {new List<int>() {0, 4, 5}, new List<int>() {1, 0, 5} } },
  { 222, new List<List<int>>() {new List<int>() {8, 4, 5}, new List<int>() {8, 5, 3}, new List<int>() {9, 0, 5}, new List<int>() {0, 3, 5} } },
  { 223, new List<List<int>>() {new List<int>() {9, 4, 5} } },
  { 224, new List<List<int>>() {new List<int>() {4, 11, 7}, new List<int>() {4, 9, 11}, new List<int>() {9, 10, 11} } },
  { 225, new List<List<int>>() {new List<int>() {0, 8, 3}, new List<int>() {4, 9, 7}, new List<int>() {9, 11, 7}, new List<int>() {9, 10, 11} } },
  { 226, new List<List<int>>() {new List<int>() {1, 10, 11}, new List<int>() {1, 11, 4}, new List<int>() {1, 4, 0}, new List<int>() {7, 4, 11} } },
  { 227, new List<List<int>>() {new List<int>() {3, 1, 4}, new List<int>() {3, 4, 8}, new List<int>() {1, 10, 4}, new List<int>() {7, 4, 11}, new List<int>() {10, 11, 4}, } },
  { 228, new List<List<int>>() {new List<int>() {4, 11, 7}, new List<int>() {9, 11, 4}, new List<int>() {9, 2, 11}, new List<int>() {9, 1, 2} } },
  { 229, new List<List<int>>() {new List<int>() {9, 7, 4}, new List<int>() {9, 11, 7}, new List<int>() {9, 1, 11}, new List<int>() {2, 11, 1}, new List<int>() {0, 8, 3}, } },
  { 230, new List<List<int>>() {new List<int>() {11, 7, 4}, new List<int>() {11, 4, 2}, new List<int>() {2, 4, 0} } },
  { 231, new List<List<int>>() {new List<int>() {11, 7, 4}, new List<int>() {11, 4, 2}, new List<int>() {8, 3, 4}, new List<int>() {3, 2, 4} } },
  { 232, new List<List<int>>() {new List<int>() {2, 9, 10}, new List<int>() {2, 7, 9}, new List<int>() {2, 3, 7}, new List<int>() {7, 4, 9} } },
  { 233, new List<List<int>>() {new List<int>() {9, 10, 7}, new List<int>() {9, 7, 4}, new List<int>() {10, 2, 7}, new List<int>() {8, 7, 0}, new List<int>() {2, 0, 7}, } },
  { 234, new List<List<int>>() {new List<int>() {3, 7, 10}, new List<int>() {3, 10, 2}, new List<int>() {7, 4, 10}, new List<int>() {1, 10, 0}, new List<int>() {4, 0, 10}, } },
  { 235, new List<List<int>>() {new List<int>() {1, 10, 2}, new List<int>() {8, 7, 4} } },
  { 236, new List<List<int>>() {new List<int>() {4, 9, 1}, new List<int>() {4, 1, 7}, new List<int>() {7, 1, 3} } },
  { 237, new List<List<int>>() {new List<int>() {4, 9, 1}, new List<int>() {4, 1, 7}, new List<int>() {0, 8, 1}, new List<int>() {8, 7, 1} } },
  { 238, new List<List<int>>() {new List<int>() {4, 0, 3}, new List<int>() {7, 4, 3} } },
  { 239, new List<List<int>>() {new List<int>() {4, 8, 7} } },
  { 240, new List<List<int>>() {new List<int>() {9, 10, 8}, new List<int>() {10, 11, 8} } },
  { 241, new List<List<int>>() {new List<int>() {3, 0, 9}, new List<int>() {3, 9, 11}, new List<int>() {11, 9, 10} } },
  { 242, new List<List<int>>() {new List<int>() {0, 1, 10}, new List<int>() {0, 10, 8}, new List<int>() {8, 10, 11} } },
  { 243, new List<List<int>>() {new List<int>() {3, 1, 10}, new List<int>() {11, 3, 10} } },
  { 244, new List<List<int>>() {new List<int>() {1, 2, 11}, new List<int>() {1, 11, 9}, new List<int>() {9, 11, 8} } },
  { 245, new List<List<int>>() {new List<int>() {3, 0, 9}, new List<int>() {3, 9, 11}, new List<int>() {1, 2, 9}, new List<int>() {2, 11, 9} } },
  { 246, new List<List<int>>() {new List<int>() {0, 2, 11}, new List<int>() {8, 0, 11} } },
  { 247, new List<List<int>>() {new List<int>() {3, 2, 11} } },
  { 248, new List<List<int>>() {new List<int>() {2, 3, 8}, new List<int>() {2, 8, 10}, new List<int>() {10, 8, 9} } },
  { 249, new List<List<int>>() {new List<int>() {9, 10, 2}, new List<int>() {0, 9, 2} } },
  { 250, new List<List<int>>() {new List<int>() {2, 3, 8}, new List<int>() {2, 8, 10}, new List<int>() {0, 1, 8}, new List<int>() {1, 10, 8} } },
  { 251, new List<List<int>>() {new List<int>() {1, 10, 2} } },
  { 252, new List<List<int>>() {new List<int>() {1, 3, 8}, new List<int>() {9, 1, 8} } },
  { 253, new List<List<int>>() {new List<int>() {0, 9, 1} } },
  { 254, new List<List<int>>() {new List<int>() {0, 3, 8} } },
  { 255, new List<List<int>>() { } }
  };

  public static List<List<int>> GetTrianglesForCaseNr(int caseNr)
  {
  return TriangleList[caseNr];
  }
}
