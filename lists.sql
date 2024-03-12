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
    label VARCHAR(32),
    description VARCHAR(255),
    CONSTRAINT fk_listid FOREIGN KEY (listid) REFERENCES lists(id),
    CONSTRAINT fk_nextid FOREIGN KEY (nextid) REFERENCES listnodes(id)
);


INSERT INTO lists (isdoublylinked) VALUES 
(FALSE), 
(TRUE);

INSERT INTO listnodes (x, y, label, listid, nextid) VALUES
(600, 150, 'A', 1, 2),
(300, 250, 'B', 1, 3),
(900, 250, 'C', 1, 4),
(200, 350, 'D', 1, 5),
(400, 350, 'E', 1, 6),
(800, 350, 'F', 1, 7),
(1000, 350, 'G', 1, 8),
(200, 350, 'H', 1, 9),
(450, 450, 'I', 1, 10),
(750, 450, 'J', 1, NULL);