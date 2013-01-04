CC = g++
CFLAGS = -Wall -Wextra
SRC = src
OBJ = obj
BIN = bin
OBJECTS = $(OBJ)/main.o
NAME = montador

all: linuxRelease

linuxRelease: CFLAGS += -s -O3
linuxRelease: montador

linuxDebug: CFLAGS += -g
linuxDebug: montador

montador: $(OBJECTS)
	$(CC) $(CFLAGS) $(OBJECTS) -o $(BIN)/$(NAME)

$(OBJ)/%.o: $(SRC)/%.cpp
	$(CC) $(CFLAGS) $< -c -o $@

clean:
	rm -f obj/*.o
	rm -f bin/$(NAME)
