-- Table: public.Invitation

-- DROP TABLE public."Invitation";

CREATE TABLE public."Invitation"
(
    "Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    author integer NOT NULL,
    createdon date NOT NULL,
    phone text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Invitation_pkey" PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."Invitation"
    OWNER to postgres;