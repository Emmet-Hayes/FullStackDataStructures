DROP TABLE IF EXISTS arraynodes;
DROP TABLE IF EXISTS arrays;

CREATE TABLE arrays (
    id SERIAL PRIMARY KEY,
    isdynamic BOOLEAN NOT NULL
);

CREATE TABLE arraynodes (
    id SERIAL PRIMARY KEY,
    x FLOAT NOT NULL,
    y FLOAT NOT NULL,
    arrayid INTEGER NOT NULL,
    index INTEGER,
    value INTEGER NOT NULL DEFAULT 0,
    label VARCHAR(32),
    description VARCHAR(255),
    CONSTRAINT fk_listid FOREIGN KEY (arrayid) REFERENCES arrays(id),
    CONSTRAINT fk_index FOREIGN KEY (index) REFERENCES arraynodes(id)
);


INSERT INTO arrays (isdynamic) VALUES 
(FALSE), 
(TRUE);

INSERT INTO arraynodes (x, y, label, arrayid, index, value) VALUES
(100, 150, 'A', 1, 1, 31),
(200, 250, 'B', 1, 2, 14),
(300, 250, 'C', 1, 3, 25),
(400, 350, 'D', 1, 4, 7),
(500, 350, 'E', 1, 5, 4),
(600, 350, 'F', 1, 6, 2),
(700, 350, 'G', 1, 7, 63),
(800, 350, 'H', 1, 8, 17),
(950, 450, 'I', 1, 9, 36),
(1050, 450, 'J', 1, 10, 1052);