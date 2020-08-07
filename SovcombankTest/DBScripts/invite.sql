-- FUNCTION: public.invite(bigint, text[])

-- DROP FUNCTION public.invite(bigint, text[]);

CREATE OR REPLACE FUNCTION public.invite(
	user_id bigint,
	phones text[])
    RETURNS void
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
AS $BODY$BEGIN
INSERT INTO public."Invitation" 
(author,phone,createdon)
SELECT user_id,
     unnest(phones), CURRENT_DATE;
END;
$BODY$;

ALTER FUNCTION public.invite(bigint, text[])
    OWNER TO postgres;
