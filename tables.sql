CREATE TYPE situacao_enum AS ENUM ('pendente', 'paga');

CREATE TABLE clientes (
    id SERIAL PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    cpf VARCHAR(11) UNIQUE NOT NULL,
    email VARCHAR(50),
    data_nascimento DATE NOT NULL,
    data_cadastro TIMESTAMP NOT NULL DEFAULT NOW()
);

CREATE TABLE dividas (
    id SERIAL PRIMARY KEY,
    id_cliente INTEGER NOT NULL,
    descricao TEXT,
    valor_total NUMERIC(10, 2) NOT NULL,
    valor_pago NUMERIC(10, 2) NOT NULL DEFAULT 0,
    data_criacao DATE NOT NULL DEFAULT CURRENT_DATE,
    data_pagamento DATE,
    situacao situacao_enum NOT NULL DEFAULT 'pendente',
    FOREIGN KEY (id_cliente) REFERENCES clientes(id) ON DELETE CASCADE
);

CREATE TABLE pagamentos (
    id SERIAL PRIMARY KEY,
    id_divida INTEGER NOT NULL,
    valor_pagamento NUMERIC(10, 2) NOT NULL,
    data_pagamento DATE NOT NULL DEFAULT CURRENT_DATE,
    descricao TEXT,
    FOREIGN KEY (id_divida) REFERENCES dividas(id) ON DELETE CASCADE
);
