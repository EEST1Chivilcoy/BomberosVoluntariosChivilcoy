// Módulo para exportar HTML a PDF usando html2pdf.js
window.pdfExport = {
    generatePdfFast: async function (elementId, fileName, orientation) {
        const element = document.getElementById(elementId);
        if (!element) {
            console.error('Elemento no encontrado:', elementId);
            return false;
        }

        try {
            const printWindow = window.open('', '_blank');
            if (!printWindow) {
                console.error('No se pudo abrir la ventana de impresión.');
                return false;
            }

            const title = fileName || 'documento.pdf';
            const pageOrientation = orientation === 'landscape' ? 'landscape' : 'portrait';
            const styles = Array.from(document.querySelectorAll('style, link[rel="stylesheet"]'))
                .map(style => style.outerHTML)
                .join('');

            printWindow.document.open();
            printWindow.document.write(`
                <html>
                <head>
                    <title>${title}</title>
                    ${styles}
                    <style>
                        @page { size: A4 ${pageOrientation}; margin: 8mm; }
                        body { margin: 0; }
                    </style>
                </head>
                <body>
                    ${element.outerHTML}
                </body>
                </html>
            `);
            printWindow.document.close();

            printWindow.onload = function () {
                printWindow.focus();
                printWindow.print();
            };

            return true;
        } catch (error) {
            console.error('Error generando PDF rápido:', error);
            return false;
        }
    },

    generatePdfPreviewFast: async function (elementId, fileName, orientation) {
        const element = document.getElementById(elementId);
        if (!element) {
            console.error('Elemento no encontrado:', elementId);
            return false;
        }

        try {
            const previewWindow = window.open('', '_blank');
            if (!previewWindow) {
                console.error('No se pudo abrir la vista previa.');
                return false;
            }

            const title = fileName || 'documento.pdf';
            const pageOrientation = orientation === 'landscape' ? 'landscape' : 'portrait';
            const styles = Array.from(document.querySelectorAll('style, link[rel="stylesheet"]'))
                .map(style => style.outerHTML)
                .join('');

            previewWindow.document.open();
            previewWindow.document.write(`
                <html>
                <head>
                    <title>${title}</title>
                    ${styles}
                    <style>
                        @page { size: A4 ${pageOrientation}; margin: 8mm; }
                        body { margin: 0; background: #f0f0f0; }
                        #pdf-content { margin-top: 10px; margin-bottom: 20px; }
                    </style>
                </head>
                <body>
                    ${element.outerHTML}
                </body>
                </html>
            `);
            previewWindow.document.close();

            return true;
        } catch (error) {
            console.error('Error abriendo vista previa rápida:', error);
            return false;
        }
    },

    generatePdf: async function (elementId, fileName, options) {
        const element = document.getElementById(elementId);
        if (!element) {
            console.error('Elemento no encontrado:', elementId);
            return false;
        }

        const defaultOptions = {
            margin: 10,
            filename: fileName || 'documento.pdf',
            image: { type: 'jpeg', quality: 0.98 },
            html2canvas: {
                scale: 2,
                useCORS: true,
                letterRendering: true,
                logging: false
            },
            jsPDF: {
                unit: 'mm',
                format: 'a4',
                orientation: 'portrait'
            },
            pagebreak: {
                mode: ['avoid-all', 'css', 'legacy'],
                before: '.page-break-before',
                after: '.page-break-after',
                avoid: '.avoid-break'
            }
        };

        const mergedOptions = { ...defaultOptions, ...options };

        try {
            await html2pdf().set(mergedOptions).from(element).save();
            return true;
        } catch (error) {
            console.error('Error generando PDF:', error);
            return false;
        }
    },

    generatePdfWithPreview: async function (elementId, fileName, orientation) {
        const element = document.getElementById(elementId);
        if (!element) {
            console.error('Elemento no encontrado:', elementId);
            return false;
        }

        const pageOrientation = orientation === 'landscape' ? 'landscape' : 'portrait';

        const options = {
            margin: [10, 10, 15, 10], // top, left, bottom, right
            filename: fileName || 'documento.pdf',
            image: { type: 'jpeg', quality: 0.98 },
            html2canvas: {
                scale: 2,
                useCORS: true,
                letterRendering: true,
                logging: false,
                scrollX: 0,
                scrollY: 0
            },
            jsPDF: {
                unit: 'mm',
                format: 'a4',
                orientation: pageOrientation
            },
            pagebreak: {
                mode: ['css', 'legacy'],
                before: '.page-break-before',
                after: '.page-break-after',
                avoid: 'tr, .avoid-break'
            }
        };

        try {
            // Generar y abrir en nueva ventana para vista previa
            const pdf = await html2pdf().set(options).from(element).toPdf().get('pdf');
            const blob = pdf.output('blob');
            const url = URL.createObjectURL(blob);
            window.open(url, '_blank');
            return true;
        } catch (error) {
            console.error('Error generando PDF:', error);
            return false;
        }
    },

    printElement: function (elementId) {
        const element = document.getElementById(elementId);
        if (!element) {
            console.error('Elemento no encontrado:', elementId);
            return;
        }

        const printWindow = window.open('', '_blank');
        printWindow.document.write('<html><head><title>Imprimir</title>');
        printWindow.document.write('<style>');
        
        // Copiar todos los estilos del documento actual
        const styles = document.querySelectorAll('style');
        styles.forEach(style => {
            printWindow.document.write(style.innerHTML);
        });
        
        printWindow.document.write('</style></head><body>');
        printWindow.document.write(element.innerHTML);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        
        printWindow.onload = function() {
            printWindow.print();
        };
    }
};
