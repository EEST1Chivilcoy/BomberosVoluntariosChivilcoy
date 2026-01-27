// Módulo para exportar HTML a PDF usando html2pdf.js
window.pdfExport = {
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

    generatePdfWithPreview: async function (elementId, fileName) {
        const element = document.getElementById(elementId);
        if (!element) {
            console.error('Elemento no encontrado:', elementId);
            return false;
        }

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
                orientation: 'portrait'
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
