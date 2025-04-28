const express = require('express');
const path = require('path');
const app = express();

app.set('trust proxy', true);

app.use((req, res, next) => {
  console.log(`Incoming ${req.method} request to ${req.url}`);
  next();
});

const staticPath = path.join(__dirname, 'dist/angular-material-rick-and-morty');
console.log('Serving static files from:', staticPath);

app.use(express.static(staticPath));

app.get('*', (req, res) => {
  console.log('Catch-all route triggered for URL:', req.url);
  res.sendFile(path.join(staticPath, 'index.html'), (err) => {
    if (err) {
      console.error('Error sending index.html:', err);
      res.status(err.status || 500).end();
    }
  });
});

const port = process.env.PORT || 8080;
app.listen(port, () => {
  console.log(`Express server running on port ${port}`);
});
