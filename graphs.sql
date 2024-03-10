DROP TABLE edges;
DROP TABLE nodes;
DROP TABLE graphs;

CREATE TABLE graphs (
    id SERIAL PRIMARY KEY,
    isdirected BOOLEAN NOT NULL
);

CREATE TABLE nodes (
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
    fromnode INTEGER NOT NULL,
    tonode INTEGER NOT NULL,
    weight FLOAT NOT NULL DEFAULT 1,
    graphid INTEGER NOT NULL,
    CONSTRAINT fk_from FOREIGN KEY (fromnode) REFERENCES nodes(id),
    CONSTRAINT fk_to FOREIGN KEY (tonode) REFERENCES nodes(id),
    CONSTRAINT fk_graphid FOREIGN KEY (graphid) REFERENCES graphs(id)
);



INSERT INTO graphs (isdirected) VALUES (TRUE), (TRUE);

INSERT INTO nodes (x, y, label, graphid) VALUES
(100, 150, 'A', 1),
(200, 250, 'B', 1),
(300, 150, 'C', 1),
(400, 250, 'D', 1),
(500, 350, 'E', 1),
(600, 450, 'F', 1),
(700, 350, 'G', 1),
(800, 450, 'H', 1),
(500, 100, 'I', 1),
(600, 150, 'J', 1),
(700, 250, 'K', 1),
(800, 300, 'L', 1);

INSERT INTO edges (fromnode, tonode, weight, graphid) VALUES
(1, 2, 1.5, 1),
(2, 3, 1.5, 1),
(3, 4, 1.5, 1),
(2, 4, 1.5, 1),
(3, 5, 1.5, 1),
(4, 6, 1.5, 1),
(5, 6, 1.5, 1),
(6, 7, 1.5, 1),
(7, 8, 1.5, 1),
(5, 7, 1.5, 1),
(6, 8, 1.5, 1),
(8, 9, 1.5, 1),
(9, 10, 1.5, 1),
(10, 11, 1.5, 1),
(11, 12, 1.5, 1),
(9, 11, 1.5, 1);