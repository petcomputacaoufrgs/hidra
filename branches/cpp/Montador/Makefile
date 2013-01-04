CC = g++
CFLAGS = -Wall -Wextra
SRC = src
OBJ = obj
BIN = bin
OBJECTS = $(OBJ)/main.o
NAME = montador

all: linuxRelease

linuxRelease: CFLAGS += -s -O3
linuxRelease: BIN = bin/linux/release
linuxRelease: objs
	#R
linuxDebug: CFLAGS += -g
linuxDebug: montador
	#D
	#O $(OBJ)
montador: $(OBJECTS)
#	$(CC) $(CFLAGS) $(objects) -o $(BIN)/$(NAME)
	#$(OBJECTS)

$(OBJ)/%.o: $(SRC)/%.cpp
	$(CC) $(CFLAGS) $< -c -o $@

clean:
	rm -f obj/*.o
	rm -f bin/$(NAME)
