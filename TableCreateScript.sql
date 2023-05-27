	
-- Table: public.user
-- DROP TABLE IF EXISTS public."user";
CREATE TABLE IF NOT EXISTS public."user"
(
    id bigint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1 ),
    name character varying COLLATE pg_catalog."default",
    phone character varying COLLATE pg_catalog."default",
    email character varying COLLATE pg_catalog."default",
    CONSTRAINT user_pkey PRIMARY KEY (id),
    CONSTRAINT user_name_key UNIQUE (name)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."user"
    OWNER to postgres;



-- Table: public.chat
-- DROP TABLE IF EXISTS public.chat;
CREATE TABLE IF NOT EXISTS public.chat
(
    id bigint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1 ),
    name character varying COLLATE pg_catalog."default",
    datecre timestamp without time zone,
    CONSTRAINT chat_pkey PRIMARY KEY (id),
    CONSTRAINT chat_name_key UNIQUE (name)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.chat
    OWNER to postgres;
	
	
	
	
-- Table: public.user2chat
-- DROP TABLE IF EXISTS public.user2chat;
CREATE TABLE IF NOT EXISTS public.user2chat
(
    userid bigint NOT NULL,
    chatid bigint NOT NULL,
    id bigint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1 ),
    CONSTRAINT user2chat_pkey PRIMARY KEY (id),
    CONSTRAINT fk_chat FOREIGN KEY (chatid)
        REFERENCES public.chat (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID,
    CONSTRAINT fk_user FOREIGN KEY (userid)
        REFERENCES public."user" (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.user2chat
    OWNER to postgres;
-- Index: fki_fk_chat

-- DROP INDEX IF EXISTS public.fki_fk_chat;

CREATE INDEX IF NOT EXISTS fki_fk_chat
    ON public.user2chat USING btree
    (chatid ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: fki_fk_user

-- DROP INDEX IF EXISTS public.fki_fk_user;

CREATE INDEX IF NOT EXISTS fki_fk_user
    ON public.user2chat USING btree
    (userid ASC NULLS LAST)
    TABLESPACE pg_default;
	
	
	
-- Table: public.message
-- DROP TABLE IF EXISTS public.message;
CREATE TABLE IF NOT EXISTS public.message
(
    id bigint NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1 ),
    chatid bigint NOT NULL,
    dt timestamp without time zone,
    body text COLLATE pg_catalog."default",
    userfrom bigint,
    CONSTRAINT message_pkey PRIMARY KEY (id),
    CONSTRAINT fk_message_chatid FOREIGN KEY (chatid)
        REFERENCES public.chat (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID,
    CONSTRAINT fk_message_userid FOREIGN KEY (userfrom)
        REFERENCES public."user" (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.message
    OWNER to postgres;
-- Index: fki_fk_message_chatid

-- DROP INDEX IF EXISTS public.fki_fk_message_chatid;

CREATE INDEX IF NOT EXISTS fki_fk_message_chatid
    ON public.message USING btree
    (chatid ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: fki_fk_message_userid

-- DROP INDEX IF EXISTS public.fki_fk_message_userid;

CREATE INDEX IF NOT EXISTS fki_fk_message_userid
    ON public.message USING btree
    (userfrom ASC NULLS LAST)
    TABLESPACE pg_default;



insert into public.user(name, phone, email)
values ('Иван Иванов', '767676', '@mail.ru'),
       ('Петр Петров', '3443', '@mail.ru'),
       ('Семен Семенов', '43563', '@mail.ru');



insert into public.chat(name, datecre)
values ('chat1', '27.05.2023'),
       ('chat2', '27.05.2023'),
       ('chat3', '27.05.2023');
	   
insert into public.user2chat(userid, chatid)
values (1, 1),
       (2, 1),
	   (2, 2),
       (3, 2),	
	   (1, 3),
       (2, 3),	   
       (3, 3);
	   
	   
	   
insert into public.message(chatid, dt, body, userfrom)
values (1, '27.05.2023', 'Привет!', 1),
       (1, '27.05.2023', 'Пока!', 2),
	   (2, '27.05.2023', 'Привет!', 2),
       (2, '27.05.2023', 'Пока!', 3),
	   (3, '27.05.2023', 'Привет!', 1),
       (3, '27.05.2023', 'Пока!', 2),	
	   (3, '27.05.2023', 'Пока!', 3);
	   