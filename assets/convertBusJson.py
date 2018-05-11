#!/usr/bin/env python
import json
from collections import OrderedDict

# OPEN BUS NAME AND BUS STOP FILE
#with open ('StopNamesByLine.json') as fileOpen:
nameFile = open ('StopNamesByLine.json')
name_data = json.load(nameFile, object_pairs_hook=OrderedDict)
#print (name_data["AB"][2])
#pprint(name_data)

# OPEN STOP COORDINATE DATA FILE
gpsFile = open('StopLocationsByLine.json')
gps_data = json.load(gpsFile, object_pairs_hook=OrderedDict)
#print (gps_data[2][3]["lat"])

#WRITE NAME, STOP, AND COORDINATE DATA TO FILE
jsonStr = ""
#print          ("{")
#fileWrite.write("{\n")
jsonStr +=       "{\n"
busLineStrArr = []
lineInd = 0
for busLine in OrderedDict(name_data):
	#print          ("\t\"" + str(busLine) + "\":")
	#fileWrite.write("\t\"" + str(busLine) + "\":\n")
	busLineStr =   "\n\t\"" + str(busLine) + "\":"
	#print          ("\t[")
	#fileWrite.write("\t[\n")
	busLineStr +=  "\n\t["
	busStopArr = []
	for stopInd in range (len(name_data[busLine])):
		#print             ("\t\t[\"" + name_data[busLine][stopInd] + "\", {\"lat\":" + str(gps_data[lineInd][stopInd]["lat"]) + ", \"lng\":" + str(gps_data[lineInd][stopInd]["lng"]) + "}]")
		#fileWrite.write   ("\t\t[\"" + name_data[busLine][stopInd] + "\", {\"lat\":" + str(gps_data[lineInd][stopInd]["lat"]) + ", \"lng\":" + str(gps_data[lineInd][stopInd]["lng"]) + "}]\n")
		busStopArr.append("\n\t\t[\"" + name_data[busLine][stopInd] + "\", {\"lat\":" + str(gps_data[lineInd][stopInd]["lat"]) + ", \"lng\":" + str(gps_data[lineInd][stopInd]["lng"]) + "}]")
	busLineStr += ",".join(busStopArr)
	#print          ("\t]\n")
	#fileWrite.write("\t]\n\n")
	busLineStr +="\n\n\t]"
	busLineStrArr.append (busLineStr)
	lineInd += 1
jsonStr += ",".join(busLineStrArr)
#print          ("}")
#fileWrite.write("}\n")
jsonStr +=       "}\n"

print (jsonStr)
fileWrite = open ("BusStops.json", "w")
fileWrite.write(jsonStr)
fileWrite.close()

