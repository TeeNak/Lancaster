

# How To Run

## Web
Run the command as follows.
```
cd Web
npm install
npm run start
```

If you want to run api side on Kestrel make an appropreate change of `serverUrl` on `environment.ts` 

e.g. Running on IIS Express

```
serverUrl: `https://${window.location.hostname}:44320`
```

e.g. Running on Kestrel https
```
serverUrl: `https://${window.location.hostname}:5001`
```



## LancasterApi

If you are running the web using a command other than `npm run start`, enter the URL for the web side in the `AllowedCorsOrigins` field of `appsettings.json`.

If you are running the web with `npm run start`, you don't need to do anything since you have already registered "http://localhost:4200".

Open the LancasterApi solution and run debug. A debug run will be started on IISExpress.

A database will be created in `(localdb)\mssqllocaldb` with the name `LancasterApiDb` at the first startup.



## Access To Web using Browser

After running both Web and LancasterApi, visit `htttp://localhost:4200` .

# Notice

If you want to run api server on IIS or IIS Express. Setting below is required.
This example extends upload size up to 500 MB

web.config

``` 
 <security>
    <requestFiltering>
        <!-- Configures IIS to accept files up to 500MB -->
        <requestLimits maxAllowedContentLength="524288000" />
    </requestFiltering>
</security>
```





