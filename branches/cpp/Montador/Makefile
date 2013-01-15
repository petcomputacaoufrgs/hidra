CC = g++
CFLAGS = -Wall -Wextra -I$(INC)
SRC = src
OBJ = obj
BIN = bin
INC = include
OBJECTS = $(OBJ)/main.o $(OBJ)/assembler.o $(OBJ)/stringer.o $(OBJ)/sha1.o $(OBJ)/instructions.o $(OBJ)/addressings.o $(OBJ)/registers.o $(OBJ)/machine.o
NAME = hidrassembler

all: release

release: CFLAGS += -s -O3
release: assembler

debug: CFLAGS += -g
debug: assembler

assembler: $(OBJECTS)
	$(CC) $(CFLAGS) $(OBJECTS) -o $(BIN)/$(NAME)

$(OBJ)/%.o: $(SRC)/%.cpp
	$(CC) $(CFLAGS) $< -c -o $@

clean:
	rm -f obj/*.o
	rm -f bin/$(NAME)
