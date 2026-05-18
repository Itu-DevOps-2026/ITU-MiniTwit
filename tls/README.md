# Initial fetch of TLS certificates

The setup of TLS for Nginx is a chicken-and-egg problem: we need cert files present for nginx to start, but we need nginx running to serve the ACME challenge for Let's Encrypt.
To solve this, we can use a temporary self-signed certificate to bootstrap the process.

The process involves the following steps:
1. Generate a temporary self-signed certificate for the domain.
2. Start all docker services except certbot.
3. Use certbot to fetch the real certificates from Let's Encrypt using the temporary certificate for validation.
4. Once the real certificates are obtained, reload Nginx to use the new certificates.
5. Start certbot in the background to handle automatic renewal of certificates.

The prerequisites for this process are that the A record for `minitwitgroupe.tech` is correctly pointing to the droplet's IP address, and that port 80 is open and accessible for Let's Encrypt to perform the HTTP-01 challenge.

The latter is already the case since Nginx is configured to listen on port 80 (as well as 443). The A record is also set up correctly.

The following steps will guide you through the process of setting up TLS for Nginx using a temporary self-signed certificate and then obtaining real certificates from Let's Encrypt.
They all have to be executed on the minitwitgroupe.tech droplet.

## Step 1: Generate Temporary Self-Signed Certificate

```bash
docker compose run --rm --entrypoint "" certbot sh -c "\
  mkdir -p /etc/letsencrypt/live/minitwitgroupe.tech && \
  openssl req -x509 -nodes -days 1 -newkey rsa:2048 \
  -keyout /etc/letsencrypt/live/minitwitgroupe.tech/privkey.pem \
  -out /etc/letsencrypt/live/minitwitgroupe.tech/fullchain.pem \
  -subj '/CN=minitwitgroupe.tech'"
```

## Step 2: Start Docker Services Except Certbot

Since `nginx` depends on all other services but `certbot`, we can start it first to ensure that the temporary certificate is in place for the HTTP-01 challenge.

```bash
docker compose up -d nginx
```

## Step 3: Obtain Real Certificates from Let's Encrypt

For this step, run certbot to perform the HTTP-01 challenge and obtain the real certificates. For it to work properly, we need to remove
the temporary certificate files to avoid conflicts.

```bash
docker compose run --rm --entrypoint "" certbot sh -c "\
  rm -rf /etc/letsencrypt/live/minitwitgroupe.tech \
          /etc/letsencrypt/archive/minitwitgroupe.tech \
          /etc/letsencrypt/renewal/minitwitgroupe.tech.conf && \
  certbot certonly --webroot -w /var/www/certbot \
  -d minitwitgroupe.tech --email <insert email> --agree-tos --no-eff-email"
```

## Step 4: Reload Nginx to Use New Certificates

```bash
docker compose exec nginx nginx -s reload
```

## Step 5: Start Certbot in Background for Automatic Renewal

```bash
docker compose up -d certbot
```

## Verification

To verify that the TLS setup is working correctly, you can use the following command _on your own machine_ to check the certificate details:

```bash
echo | openssl s_client -connect minitwitgroupe.tech:443 -servername minitwitgroupe.tech 2>/dev/null | openssl x509 -noout -issuer -dates
```

It should show the issuer as Let's Encrypt and the validity dates for the certificate. You can also visit `https://minitwitgroupe.tech` in a web browser to check that the site is served over HTTPS without any certificate warnings.
