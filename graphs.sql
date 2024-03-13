DROP TABLE IF EXISTS edges;
DROP TABLE IF EXISTS vertices;
DROP TABLE IF EXISTS graphs;

CREATE TABLE graphs (
    id SERIAL PRIMARY KEY,
    isdirected BOOLEAN NOT NULL
);

CREATE TABLE vertices (
    id SERIAL PRIMARY KEY,
    x FLOAT NOT NULL,
    y FLOAT NOT NULL,
    graphid INTEGER NOT NULL,
    value INTEGER NOT NULL DEFAULT 0,
    label VARCHAR(32),
    description VARCHAR(255),
    CONSTRAINT fk_graphid FOREIGN KEY (graphid) REFERENCES graphs(id)
);

CREATE TABLE edges (
    id SERIAL PRIMARY KEY,
    fromvertex INTEGER NOT NULL,
    tovertex INTEGER NOT NULL,
    weight FLOAT NOT NULL DEFAULT 1,
    graphid INTEGER NOT NULL,
    CONSTRAINT fk_from FOREIGN KEY (fromvertex) REFERENCES vertices(id),
    CONSTRAINT fk_to FOREIGN KEY (tovertex) REFERENCES vertices(id),
    CONSTRAINT fk_graphid FOREIGN KEY (graphid) REFERENCES graphs(id)
);


INSERT INTO graphs (isdirected) VALUES 
(FALSE), 
(TRUE);

INSERT INTO vertices (x, y, label, graphid, value) VALUES
(100, 150, 'A', 1, 14),
(200, 250, 'B', 1, 31),
(300, 150, 'C', 1, 25),
(400, 250, 'D', 1, 37),
(500, 350, 'E', 1, 200),
(600, 450, 'F', 1, 36),
(700, 350, 'G', 1, 16),
(800, 450, 'H', 1, 0),
(900, 100, 'I', 1, -3),
(1000, 150, 'J', 1, -7),
(1100, 250, 'K', 1, 13),
(1100, 350, 'L', 1, 13);

INSERT INTO edges (fromvertex, tovertex, weight, graphid) VALUES
(1, 2, 2, 1),
(1, 3, 3, 1),
(2, 3, 5, 1),
(3, 4, 1, 1),
(4, 6, 1, 1),
(5, 6, 3, 1),
(6, 7, 4, 1),
(7, 8, 3, 1),
(5, 7, 2, 1),
(6, 8, 1, 1),
(8, 9, 1, 1),
(9, 10, 1.5, 1),
(10, 11, 3.6, 1),
(9, 11, 4.16, 1);