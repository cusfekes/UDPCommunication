-- Table: public.UDPLog

-- DROP TABLE IF EXISTS public."UDPLog";

CREATE TABLE IF NOT EXISTS public."UDPLog"
(
    "Id" character(36) COLLATE pg_catalog."default" NOT NULL,
    "Message" character(255) COLLATE pg_catalog."default" NOT NULL,
    "LogDate" date NOT NULL,
    "IpAddress" character(50) COLLATE pg_catalog."default" NOT NULL,
    "PortNumber" integer NOT NULL,
    "LogDirection" character(15) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "UDPLog_pkey" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."UDPLog"
    OWNER to postgres;