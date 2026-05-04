## Overview
This project extracts and processes Diablo 2 item data using OCR and parsing logic.
The data can optionally be stored in a MySQL database.

Main features:
- OCR extraction from images
- Text parsing into structured item data
- CRUD operations for storing items (requires database)
- Sorting 

## Running the project

The application supports three modes:

- `start` ? starts the console application without OCR or parsing input
- `parse` ? parses data from a text file 
- `ocr` ? extracts data from images

### Example:
dotnet run ocr <path-to-images>
dotnet run parse <path-to-txt-file> 

## Database

The project uses a MySQL database for storing parsed items.

Note:
- The database is not included in this repository
- To use database features, you need to configure your own MySQL instance

## Future improvements
- Add Docker support for database setup
- API layer
- Add Discord bot integration