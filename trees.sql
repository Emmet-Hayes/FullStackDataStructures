DROP TABLE IF EXISTS treenodes;
DROP TABLE IF EXISTS trees;

CREATE TABLE trees (
    id SERIAL PRIMARY KEY,
    isbst BOOLEAN NOT NULL,
    isbalanced BOOLEAN NOT NULL
);

CREATE TABLE treenodes (
    id SERIAL PRIMARY KEY,
    x FLOAT NOT NULL,
    y FLOAT NOT NULL,
    treeid INTEGER NOT NULL,
    parentid INTEGER,
    label VARCHAR(32),
    description VARCHAR(255),
    CONSTRAINT fk_graphid FOREIGN KEY (treeid) REFERENCES trees(id),
    CONSTRAINT fk_parentid FOREIGN KEY (parentid) REFERENCES treenodes(id)
);


INSERT INTO trees (isbst, isbalanced) VALUES 
(FALSE, FALSE), 
(TRUE, TRUE);

INSERT INTO treenodes (x, y, label, treeid, parentid) VALUES
(600, 150, 'A', 1, NULL),
(300, 250, 'B', 1, 1),
(900, 250, 'C', 1, 1),
(200, 350, 'D', 1, 2),
(400, 350, 'E', 1, 2),
(800, 350, 'F', 1, 3),
(1000, 350, 'G', 1, 3),
(200, 350, 'H', 1, 4),
(450, 450, 'I', 1, 5),
(750, 450, 'J', 1, 6);