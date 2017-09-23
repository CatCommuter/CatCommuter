#import
rf = open ("C1 Line Fall 2017.csv", "r");

#read data into 2 dimensional array
stops = [["xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx" for i in range(14)] for j in range(38)];

stopInd = 0;
loopInd = 0;
for line in rf:
    stopInd = 0;
    
    for term in line:
        stops[stopInd][loopInd] = term;
        stopInd += 1;

    loopInd += 1;

#write data to console
for row in stops:
    for column in row:
        print (column);

