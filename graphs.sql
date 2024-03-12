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

INSERT INTO vertices (x, y, label, graphid) VALUES
(100, 150, 'A', 1),
(200, 250, 'B', 1),
(300, 150, 'C', 1),
(400, 250, 'D', 1),
(500, 350, 'E', 1),
(600, 450, 'F', 1),
(700, 350, 'G', 1),
(800, 450, 'H', 1),
(900, 100, 'I', 1),
(1000, 150, 'J', 1),
(1100, 250, 'K', 1),
(1100, 350, 'L', 1);

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