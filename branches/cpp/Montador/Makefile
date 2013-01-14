CC = g++
CFLAGS = -Wall -Wextra -I$(INC)
SRC = src
OBJ = obj
BIN = bin
INC = include
OBJECTS = $(OBJ)/main.o $(OBJ)/montador.o $(OBJ)/stringer.o $(OBJ)/sha1.o
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
