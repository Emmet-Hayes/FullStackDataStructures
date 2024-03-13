DROP TABLE IF EXISTS listnodes;
DROP TABLE IF EXISTS lists;

CREATE TABLE lists (
    id SERIAL PRIMARY KEY,
    isdoublylinked BOOLEAN NOT NULL
);

CREATE TABLE listnodes (
    id SERIAL PRIMARY KEY,
    x FLOAT NOT NULL,
    y FLOAT NOT NULL,
    listid INTEGER NOT NULL,
    nextid INTEGER,
    value INTEGER NOT NULL DEFAULT 0,
    label VARCHAR(32),
    description VARCHAR(255),
    CONSTRAINT fk_listid FOREIGN KEY (listid) REFERENCES lists(id),
    CONSTRAINT fk_nextid FOREIGN KEY (nextid) REFERENCES listnodes(id)
);


INSERT INTO lists (isdoublylinked) VALUES 
(FALSE), 
(TRUE);

INSERT INTO listnodes (x, y, label, listid, nextid, value) VALUES
(100, 150, 'A', 1, 2, 0),
(200, 250, 'B', 1, 3, 3),
(300, 250, 'C', 1, 4, 1),
(400, 350, 'D', 1, 5, 4),
(500, 350, 'E', 1, 6, 2),
(600, 350, 'F', 1, 7, 1),
(700, 350, 'G', 1, 8, 5),
(800, 350, 'H', 1, 9, 6),
(900, 450, 'I', 1, 10, 4),
(1000, 450, 'J', 1, NULL, 8);