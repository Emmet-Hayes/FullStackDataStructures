DROP TABLE IF EXISTS dictnodes;
DROP TABLE IF EXISTS dicts;

CREATE TABLE dicts (
    id SERIAL PRIMARY KEY,
    hashfunction VARCHAR(32) NOT NULL
);

CREATE TABLE dictnodes (
    id SERIAL PRIMARY KEY,
    x FLOAT NOT NULL,
    y FLOAT NOT NULL,
    dictid INTEGER NOT NULL,
    key VARCHAR(10) UNIQUE NOT NULL ,
    value VARCHAR(16),
    label VARCHAR(32),
    description VARCHAR(255),
    CONSTRAINT fk_dictid FOREIGN KEY (dictid) REFERENCES dicts(id)
);


INSERT INTO dicts (hashfunction) VALUES 
('basic bucket'), 
('something else');

INSERT INTO dictnodes (x, y, label, dictid, key, value) VALUES
(100,  450, 'A', 1, 'Bulbasaur', 'Venusaur'),
(200,  450, 'B', 1, 'Charmander', 'Charizard'),
(300,  450, 'C', 1, 'Squirtle', 'Blastoise'),
(400,  450, 'D', 1, 'Abra', 'Alakazam'),
(500,  450, 'E', 1, 'Machop', 'Machamp'),
(600,  450, 'F', 1, 'Gastly', 'Gengar'),
(700,  450, 'G', 1, 'Pidgey', 'Pidgeot'),
(800,  450, 'H', 1, 'NidoranM', 'Nidoking'),
(900,  450, 'I', 1, 'Poliwag', 'Poliwrath'),
(1000, 450, 'J', 1, 'Bellsprout', 'Victreebel');