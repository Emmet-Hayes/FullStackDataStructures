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
    label VARCHAR(32),
    description VARCHAR(255),
    CONSTRAINT fk_listid FOREIGN KEY (arrayid) REFERENCES arrays(id),
    CONSTRAINT fk_index FOREIGN KEY (index) REFERENCES arraynodes(id)
);


INSERT INTO arrays (isdynamic) VALUES 
(FALSE), 
(TRUE);

INSERT INTO arraynodes (x, y, label, arrayid, index) VALUES
(600, 150, 'A', 1, 1),
(300, 250, 'B', 1, 2),
(900, 250, 'C', 1, 3),
(200, 350, 'D', 1, 4),
(400, 350, 'E', 1, 5),
(800, 350, 'F', 1, 6),
(1000, 350, 'G', 1, 7),
(200, 350, 'H', 1, 8),
(450, 450, 'I', 1, 9),
(750, 450, 'J', 1, 10);