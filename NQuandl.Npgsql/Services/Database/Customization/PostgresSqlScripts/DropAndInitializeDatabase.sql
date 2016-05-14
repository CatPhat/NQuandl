--
-- PostgreSQL database dump
--

-- Dumped from database version 9.5.1
-- Dumped by pg_dump version 9.5.1

-- Started on 2016-05-13 13:12:03 PDT

SET statement_timeout = 0;
SET lock_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

SET search_path = public, pg_catalog;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 190 (class 1259 OID 169388)
-- Name: countries; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE countries (
    name text,
    iso31661alpha3 text,
    iso31661numeric integer,
    iso31661alpha2 text,
    country_flag_url text,
    altname text,
    iso4217_currency_alphabetic_code text,
    iso4217_country_name text,
    iso4217_minor_units integer,
    iso4217_currency_name text,
    iso4217_currency_numeric_code integer
);


ALTER TABLE countries OWNER TO postgres;

--
-- TOC entry 192 (class 1259 OID 169419)
-- Name: country_currencies; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE country_currencies (
    id character varying(3) NOT NULL,
    value character varying(64) NOT NULL
);


ALTER TABLE country_currencies OWNER TO postgres;

--
-- TOC entry 193 (class 1259 OID 177578)
-- Name: data_okfn_org; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE data_okfn_org (
    name character varying(44) NOT NULL,
    name_fr character varying(44) NOT NULL,
    iso31661alpha2 character varying(2) NOT NULL,
    iso31661alpha3 character varying(3) NOT NULL,
    iso31661numeric integer NOT NULL,
    itu character varying(3),
    marc character varying(14),
    wmo character varying(2),
    ds character varying(3),
    dial character varying(17),
    fifa character varying(15),
    fips character varying(26),
    gaul character varying(6),
    ioc character varying(3),
    currency_alphabetic_code character varying(3),
    currency_country_name character varying(44),
    currency_minor_unit integer,
    currency_name character varying(29),
    currency_numeric_code integer,
    is_independent character varying(22) NOT NULL
);


ALTER TABLE data_okfn_org OWNER TO postgres;

--
-- TOC entry 183 (class 1259 OID 32843)
-- Name: databases; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE databases (
    id integer NOT NULL,
    database_code text,
    datasets_count integer NOT NULL,
    description text,
    downloads bigint NOT NULL,
    image text,
    name text,
    premium boolean NOT NULL
);


ALTER TABLE databases OWNER TO postgres;

--
-- TOC entry 182 (class 1259 OID 32841)
-- Name: database_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE "database_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "database_Id_seq" OWNER TO postgres;

--
-- TOC entry 2178 (class 0 OID 0)
-- Dependencies: 182
-- Name: database_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE "database_Id_seq" OWNED BY databases.id;


--
-- TOC entry 189 (class 1259 OID 62908)
-- Name: database_datasets; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE database_datasets (
    id integer NOT NULL,
    database_code text,
    dataset_code text,
    quandl_code text,
    description text
);


ALTER TABLE database_datasets OWNER TO postgres;

--
-- TOC entry 188 (class 1259 OID 62906)
-- Name: database_datasets_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE database_datasets_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE database_datasets_id_seq OWNER TO postgres;

--
-- TOC entry 2179 (class 0 OID 0)
-- Dependencies: 188
-- Name: database_datasets_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE database_datasets_id_seq OWNED BY database_datasets.id;


--
-- TOC entry 197 (class 1259 OID 185772)
-- Name: database_status; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE database_status (
    id integer NOT NULL,
    database_id integer,
    database_name text,
    datasets_available integer,
    datasets_downloaded integer,
    datasets_last_checked timestamp without time zone,
    database_marked_for_download boolean
);


ALTER TABLE database_status OWNER TO postgres;

--
-- TOC entry 196 (class 1259 OID 185770)
-- Name: database_status_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE database_status_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE database_status_id_seq OWNER TO postgres;

--
-- TOC entry 2180 (class 0 OID 0)
-- Dependencies: 196
-- Name: database_status_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE database_status_id_seq OWNED BY database_status.id;


--
-- TOC entry 187 (class 1259 OID 32866)
-- Name: datasets; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE datasets (
    id integer NOT NULL,
    code text,
    database_code text,
    database_id integer,
    description text,
    end_date timestamp without time zone,
    frequency text,
    name text,
    refreshed_at timestamp without time zone,
    start_date timestamp without time zone,
    data jsonb,
    column_names jsonb
);


ALTER TABLE datasets OWNER TO postgres;

--
-- TOC entry 186 (class 1259 OID 32864)
-- Name: dataset_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE "dataset_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "dataset_Id_seq" OWNER TO postgres;

--
-- TOC entry 2181 (class 0 OID 0)
-- Dependencies: 186
-- Name: dataset_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE "dataset_Id_seq" OWNED BY datasets.id;


--
-- TOC entry 195 (class 1259 OID 177585)
-- Name: dataset_countries; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE dataset_countries (
    id integer NOT NULL,
    dataset_id integer,
    iso31661alpha3 text,
    association text
);


ALTER TABLE dataset_countries OWNER TO postgres;

--
-- TOC entry 194 (class 1259 OID 177583)
-- Name: dataset_countries_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE dataset_countries_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE dataset_countries_id_seq OWNER TO postgres;

--
-- TOC entry 2182 (class 0 OID 0)
-- Dependencies: 194
-- Name: dataset_countries_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE dataset_countries_id_seq OWNED BY dataset_countries.id;


--
-- TOC entry 191 (class 1259 OID 169401)
-- Name: iso_3166_1_country_codes; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE iso_3166_1_country_codes (
    id character varying(2) NOT NULL,
    value character varying(64) NOT NULL
);


ALTER TABLE iso_3166_1_country_codes OWNER TO postgres;

--
-- TOC entry 185 (class 1259 OID 32854)
-- Name: raw_responses; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE raw_responses (
    id integer NOT NULL,
    creation_date timestamp with time zone DEFAULT now() NOT NULL,
    request_uri text,
    response_content jsonb
);


ALTER TABLE raw_responses OWNER TO postgres;

--
-- TOC entry 184 (class 1259 OID 32852)
-- Name: raw_response_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE "raw_response_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "raw_response_Id_seq" OWNER TO postgres;

--
-- TOC entry 2183 (class 0 OID 0)
-- Dependencies: 184
-- Name: raw_response_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE "raw_response_Id_seq" OWNED BY raw_responses.id;


--
-- TOC entry 2034 (class 2604 OID 62911)
-- Name: id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY database_datasets ALTER COLUMN id SET DEFAULT nextval('database_datasets_id_seq'::regclass);


--
-- TOC entry 2036 (class 2604 OID 185775)
-- Name: id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY database_status ALTER COLUMN id SET DEFAULT nextval('database_status_id_seq'::regclass);


--
-- TOC entry 2035 (class 2604 OID 177588)
-- Name: id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY dataset_countries ALTER COLUMN id SET DEFAULT nextval('dataset_countries_id_seq'::regclass);


--
-- TOC entry 2032 (class 2604 OID 32857)
-- Name: id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY raw_responses ALTER COLUMN id SET DEFAULT nextval('"raw_response_Id_seq"'::regclass);


--
-- TOC entry 2038 (class 2606 OID 32851)
-- Name: PK_Database; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY databases
    ADD CONSTRAINT "PK_Database" PRIMARY KEY (id);


--
-- TOC entry 2044 (class 2606 OID 32874)
-- Name: PK_Dataset; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY datasets
    ADD CONSTRAINT "PK_Dataset" PRIMARY KEY (id);


--
-- TOC entry 2040 (class 2606 OID 32863)
-- Name: PK_RawResponse; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY raw_responses
    ADD CONSTRAINT "PK_RawResponse" PRIMARY KEY (id);


--
-- TOC entry 2047 (class 2606 OID 62916)
-- Name: database_datasets_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY database_datasets
    ADD CONSTRAINT database_datasets_pkey PRIMARY KEY (id);


--
-- TOC entry 2058 (class 2606 OID 185780)
-- Name: database_status_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY database_status
    ADD CONSTRAINT database_status_pkey PRIMARY KEY (id);


--
-- TOC entry 2056 (class 2606 OID 177593)
-- Name: dataset_countries_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY dataset_countries
    ADD CONSTRAINT dataset_countries_pkey PRIMARY KEY (id);


--
-- TOC entry 2050 (class 2606 OID 169405)
-- Name: list_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY iso_3166_1_country_codes
    ADD CONSTRAINT list_pkey PRIMARY KEY (id);


--
-- TOC entry 2052 (class 2606 OID 169423)
-- Name: list_pkey1; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY country_currencies
    ADD CONSTRAINT list_pkey1 PRIMARY KEY (id);


--
-- TOC entry 2054 (class 2606 OID 177582)
-- Name: mytable_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY data_okfn_org
    ADD CONSTRAINT mytable_pkey PRIMARY KEY (name);


--
-- TOC entry 2045 (class 1259 OID 71083)
-- Name: database_code_idx; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX database_code_idx ON database_datasets USING btree (database_code);


--
-- TOC entry 2048 (class 1259 OID 62926)
-- Name: quandl_code_idx; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX quandl_code_idx ON database_datasets USING btree (quandl_code);


--
-- TOC entry 2041 (class 1259 OID 79276)
-- Name: raw_response_dataset_code_idx; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX raw_response_dataset_code_idx ON raw_responses USING gin ((((response_content -> 'dataset'::text) -> 'dataset_code'::text)));


--
-- TOC entry 2042 (class 1259 OID 79275)
-- Name: raw_response_dataset_idx; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX raw_response_dataset_idx ON raw_responses USING gin (((response_content -> 'dataset'::text)));


--
-- TOC entry 2059 (class 2606 OID 177594)
-- Name: dataset_countries_dataset_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY dataset_countries
    ADD CONSTRAINT dataset_countries_dataset_id_fkey FOREIGN KEY (dataset_id) REFERENCES datasets(id);


-- Completed on 2016-05-13 13:12:03 PDT

--
-- PostgreSQL database dump complete
--

