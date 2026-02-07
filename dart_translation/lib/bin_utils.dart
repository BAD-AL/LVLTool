/// Binary utilities for LVL/UCFB data.
/// Translated from C# CoreCode/BinUtils.cs for Linux/CLI use.
/// Byte data uses [Uint8List]; [List<int>] parameters accept [Uint8List] (implements [List<int>]).

import 'dart:typed_data';

/// Searches through [data] for [givenBytes].
/// Returns location of givenBytes, -1 if not found.
int getLocationOfGivenBytes(
    int startLocation, List<int> givenBytes, List<int> data, int maxDistance) {
  int retVal = -1;
  int num = data.length - givenBytes.length;
  for (int location = startLocation; location < num; location++) {
    if (_find(givenBytes, location, data)) {
      retVal = location;
      break;
    } else if (maxDistance < location - startLocation) {
      break;
    }
  }
  return retVal;
}

bool _find(List<int> hexNumber, int location, List<int> data) {
  int index = 0;
  while (index < hexNumber.length &&
      hexNumber[index] == data[location + index]) {
    index++;
  }
  return index == hexNumber.length;
}

/// Same for Uint8List (byte array).
int getLocationOfGivenBytesFromBytes(
    int startLocation, List<int> givenBytes, List<int> data, int maxDistance) {
  return getLocationOfGivenBytes(startLocation, givenBytes, data, maxDistance);
}

String getByteString(int location, List<int> data, int numBytes) {
  if (location + numBytes > data.length) return '';
  if (location < 0) location = 0;
  final buffer = StringBuffer();
  for (int l = location; l < location + numBytes; l++) {
    if (data[l] != 0) {
      buffer.write(String.fromCharCode(data[l]));
    }
  }
  return buffer.toString();
}

/// Number/length is stored little-endian (LSB first). Result as 16-bit unsigned.
int get2ByteNumberAtLocation(int loc, List<int> data) {
  int b0 = data[loc] & 0xff;
  int b1 = data[loc + 1] & 0xff;
  return (b0 + (b1 << 8)) & 0xFFFF;
}

/// 4-byte number at location (little-endian). Result as 32-bit unsigned.
int getNumberAtLocation(int loc, List<int> data) {
  int b0 = data[loc] & 0xff;
  int b1 = data[loc + 1] & 0xff;
  int b2 = data[loc + 2] & 0xff;
  int b3 = data[loc + 3] & 0xff;
  return (b0 + (b1 << 8) + (b2 << 16) + (b3 << 24)) & 0xFFFFFFFF;
}

void writeNumberAtLocation(int loc, int num, List<int> data) {
  Uint8List encoded = encodeNumber(num);
  data[loc] = encoded[0];
  data[loc + 1] = encoded[1];
  data[loc + 2] = encoded[2];
  data[loc + 3] = encoded[3];
}

void write2ByteNumberAtLocation(int loc, int num, List<int> data) {
  Uint8List encoded = encodeNumber(num);
  data[loc] = encoded[0];
  data[loc + 1] = encoded[1];
}

/// Encodes int as 4 little-endian bytes.
Uint8List encodeNumber(int num) {
  return Uint8List(4)
    ..[0] = num & 0x000000FF
    ..[1] = (num & 0x0000FF00) >> 8
    ..[2] = (num & 0x00FF0000) >> 16
    ..[3] = (num & 0xFF000000) >> 24;
}

bool binCompare(List<int> littleData, List<int> bigData, int location) {
  for (int i = 0; i < littleData.length; i++) {
    if (littleData[i] != bigData[i + location]) return false;
  }
  return true;
}
