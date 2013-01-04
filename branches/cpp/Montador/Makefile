CC = g++
CFLAGS = -Wall -Wextra
SRC = src
OBJ = obj
BIN = bin
OBJECTS = $(OBJ)/main.o
NAME = montador

all: release

release: CFLAGS += -s -O3
release: montador

debug: CFLAGS += -g
debug: montador

montador: $(OBJECTS)
	$(CC) $(CFLAGS) $(OBJECTS) -o $(BIN)/$(NAME)

$(OBJ)/%.o: $(SRC)/%.cpp
	$(CC) $(CFLAGS) $< -c -o $@

clean:
	rm -f obj/*.o
	rm -f bin/$(NAME)
